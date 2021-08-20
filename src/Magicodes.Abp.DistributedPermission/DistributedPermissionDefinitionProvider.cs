using Magicodes.Abp.DistributedPermission.Dto;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Caching;

namespace Magicodes.Abp.DistributedPermission
{
    /// <summary>
    /// 分布式权限定义提供程序（请自行启用分布式Redis缓存）
    /// </summary>
    public class DistributedPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        private readonly ILogger<DistributedPermissionDefinitionProvider> logger;
        private const string cacheKey = "PermissionGroups";
        private readonly IDistributedCache<Dictionary<string, PermissionGroupDto>> _cache;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="cache"></param>
        public DistributedPermissionDefinitionProvider(ILogger<DistributedPermissionDefinitionProvider> logger, IDistributedCache<Dictionary<string, PermissionGroupDto>> cache)
        {
            this.logger = logger;
            _cache = cache;
        }
        public override void Define(IPermissionDefinitionContext context)
        {
            logger.LogInformation($"{nameof(DistributedPermissionDefinitionProvider)}...");
            var permissionDefinitionContext = context as PermissionDefinitionContext;
            Dictionary<string, PermissionGroupDto> distributedPermissions = _cache.GetAsync(cacheKey).Result;
            if (distributedPermissions == null)
            {
                distributedPermissions = new Dictionary<string, PermissionGroupDto>();
                foreach (var item in permissionDefinitionContext.Groups)
                {
                    var groupDto = new PermissionGroupDto()
                    {
                        Permissions = new List<PermissionDto>()
                    };
                    foreach (var childItem in item.Value.Permissions)
                    {
                        groupDto.Permissions.Add(new PermissionDto()
                        {
                            
                        });
                    }
                    //distributedPermissions.Add(item.Key,)
                }
            }
            else
            {
                //TODO:添加配置控制
                //添加权限组
                foreach (var item in distributedPermissions)
                {
                    if (!permissionDefinitionContext.Groups.ContainsKey(item.Key))
                    {
                        var groups = context.AddGroup(item.Value.Name, item.Value.DisplayName, item.Value.MultiTenancySide);
                        foreach (var childItem in item.Value.Permissions)
                        {
                            groups.AddPermission(childItem.Name, childItem.DisplayName, childItem.MultiTenancySide, childItem.IsEnabled);
                        }
                    }
                }
                //更新权限定义字典
                foreach (var item in permissionDefinitionContext.Groups)
                {
                    if (distributedPermissions.ContainsKey(item.Key))
                    {
                        //distributedPermissions[item.Key] = item.Value;
                    }
                    else
                    {
                        //distributedPermissions.Add(item.Key, item.Value);
                    }
                }

            }
            _cache.SetAsync(
                   cacheKey,
                   distributedPermissions,
                       new DistributedCacheEntryOptions
                       {
                           AbsoluteExpiration = DateTimeOffset.Now.AddYears(2)
                       }
                   ).Wait();
        }
    }
}
