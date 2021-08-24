using Magicodes.Abp.DistributedPermission.Dto;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Caching;
using System.Linq;
using Volo.Abp.Localization;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Localization;
using Volo.Abp;

namespace Magicodes.Abp.DistributedPermission
{
    /// <summary>
    /// 分布式权限定义提供程序（请自行启用分布式Redis缓存）
    /// </summary>
    public class DistributedPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        private readonly ILogger<DistributedPermissionDefinitionProvider> logger;
        private const string cacheKey = "PermissionGroups";
        private readonly IDistributedCache<PermissionCacheDto> _cache;
        private readonly IStringLocalizerFactory stringLocalizerFactory;
        private readonly AbpLocalizationOptions abpLocalizationOptions;

        private IStringLocalizer _localizer;
        protected IStringLocalizer Localizer
        {
            get
            {
                if (_localizer == null)
                {
                    _localizer = CreateLocalizer();
                }

                return _localizer;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="cache"></param>
        /// <param name="abpLocalizationOptions"></param>
        public DistributedPermissionDefinitionProvider(ILogger<DistributedPermissionDefinitionProvider> logger, IDistributedCache<PermissionCacheDto> cache,
          IOptions<AbpLocalizationOptions> abpLocalizationOptions,
          IStringLocalizerFactory stringLocalizerFactory)
        {
            this.logger = logger;
            _cache = cache;
            this.stringLocalizerFactory = stringLocalizerFactory;
            this.abpLocalizationOptions = abpLocalizationOptions.Value;
        }
        public override void Define(IPermissionDefinitionContext context)
        {
            logger.LogInformation($"{nameof(DistributedPermissionDefinitionProvider)}...");
            var permissionDefinitionContext = context as PermissionDefinitionContext;
            var distributedPermissionCache = _cache.GetAsync(cacheKey).Result;
            if (distributedPermissionCache == null || distributedPermissionCache.PermissionGroups == null || distributedPermissionCache.PermissionGroups.Count == 0)
            {
                distributedPermissionCache = new PermissionCacheDto()
                {
                    PermissionGroups = new List<PermissionGroupDto>(permissionDefinitionContext.Groups.Count)
                };
                SetGroupPermissionCache(permissionDefinitionContext, distributedPermissionCache);
            }
            else
            {
                //TODO:添加配置控制

                SetGroupPermission(context, permissionDefinitionContext, distributedPermissionCache);

                SetGroupPermissionCache(permissionDefinitionContext, distributedPermissionCache);
            }
            _cache.SetAsync(
                   cacheKey,
                   distributedPermissionCache,
                       new DistributedCacheEntryOptions
                       {
                           AbsoluteExpiration = DateTimeOffset.Now.AddYears(2)
                       }
                   ).Wait();
        }

        /// <summary>
        /// 定义权限组
        /// </summary>
        /// <param name="context"></param>
        /// <param name="permissionDefinitionContext"></param>
        /// <param name="distributedPermissionCache"></param>
        private void SetGroupPermission(IPermissionDefinitionContext context, PermissionDefinitionContext permissionDefinitionContext, PermissionCacheDto distributedPermissionCache)
        {
            //添加权限组
            foreach (var item in distributedPermissionCache.PermissionGroups)
            {
                if (!permissionDefinitionContext.Groups.ContainsKey(item.Name))
                {
                    var groups = context.AddGroup(item.Name, L(item.DisplayName), item.MultiTenancySide);
                    foreach (var childItem in item.Permissions)
                    {
                        var permission = groups.AddPermission(childItem.Name, L(childItem.DisplayName), childItem.MultiTenancySide, childItem.IsEnabled);
                        SetPermission(permission, childItem);
                    }
                }
            }
        }

        /// <summary>
        /// 定义权限
        /// </summary>
        /// <param name="permission"></param>
        /// <param name="childItem"></param>
        private void SetPermission(PermissionDefinition permission, PermissionDto childItem)
        {
            if (childItem.Children != null && childItem.Children != null && childItem.Children.Count > 0)
            {
                foreach (var item in childItem.Children)
                {
                    var childPermission = permission.AddChild(item.Name, L(item.DisplayName), item.MultiTenancySide, item.IsEnabled);
                    if (item.Children != null && item.Children != null && item.Children.Count > 0)
                        SetPermission(childPermission, item);
                }

            }
        }

        private void SetGroupPermissionCache(PermissionDefinitionContext permissionDefinitionContext, PermissionCacheDto distributedPermissionCache)
        {
            var distributedPermissions = distributedPermissionCache.PermissionGroups.ToList();
            foreach (var item in permissionDefinitionContext.Groups)
            {
                logger.LogInformation($"PermissionGroup:{item.Value.Name}");
                var groupDto = new PermissionGroupDto()
                {
                    Permissions = new List<PermissionDto>(),
                    DisplayName = Localizer[item.Value.Name],
                    MultiTenancySide = item.Value.MultiTenancySide,
                    Name = item.Value.Name
                };
                foreach (var childItem in item.Value.Permissions)
                {
                    AddPermission(groupDto, childItem);
                }
                var prevGroupDto = distributedPermissions.FirstOrDefault(p => p.Name == item.Key);
                if (prevGroupDto == null)
                {
                    distributedPermissionCache.PermissionGroups.Add(groupDto);
                }
                else
                {
                    distributedPermissions[distributedPermissions.IndexOf(prevGroupDto)] = groupDto;
                }
            }
        }

        private void AddPermission(PermissionGroupDto groupDto, PermissionDefinition childItem, PermissionDto parentPermission = null)
        {
            logger.LogInformation($"Permission:{childItem.Name}");
            var permission = new PermissionDto()
            {
                DisplayName = Localizer[childItem.Name],
                MultiTenancySide = childItem.MultiTenancySide,
                Name = childItem.Name,
                IsEnabled = childItem.IsEnabled
            };
            if (parentPermission != null)
                parentPermission.Children.Add(permission);
            else
                groupDto.Permissions.Add(permission);

            if (childItem.Children != null && childItem.Children.Count > 0)
            {
                permission.Children = new List<PermissionDto>(childItem.Children.Count);
                foreach (var item in childItem.Children)
                {
                    AddPermission(groupDto, item, permission);
                }
            }
        }

        private LocalizableString L(string name)
        {
            return new LocalizableString(abpLocalizationOptions.DefaultResourceType, name);
        }

        protected virtual IStringLocalizer CreateLocalizer()
        {
            var localizer = stringLocalizerFactory.CreateDefaultOrNull();
            if (localizer == null)
            {
                throw new AbpException($"Set {nameof(LocalizationResource)} or define the default localization resource type (by configuring the {nameof(AbpLocalizationOptions)}.{nameof(AbpLocalizationOptions.DefaultResourceType)}) to be able to use the {nameof(L)} object!");
            }

            return localizer;
        }
    }
}
