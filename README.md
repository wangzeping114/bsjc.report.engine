### 项目分层

```
- N6.Bsjc.ReportDesigner  报表设计器站点
- N6.Bsjc.ReportDocumentViewer 报表查看器站点
- N6.Bsjc.Reporting.Domain 报表领域层(提供报表查看器和报表设计器的支持)
- N6.Bsjc.Reporting.Domain.Shared 报表领域共享层(定义通用的DTO和枚举)  
- N6.Bsjc.Reporting.Domain.Test 领域层单元测试
- N6.Bsjc.Reporting.HttpApi.Client 报表数据访问层(远程接口调用)
- N6.Bsjc.Reporting.HttpApi.Client.Contracts  报表数据访问层契约(定义抽象类和接口,DTO,枚举)
- N6.Core.Abp.Client.Contracts 框架层契约的数据访问层(提供通用的远程访问方式,目前采用的Refit)
- N6.Core.Abp.Client.HttpApi 框架层契约的实现层 (提供通用的远程访问方式,目前采用的Refit)
```
### 开发环境

- [必须] .NET 5 
- [必须] Visual Studio 2019 / VS Code
- [必须] DevExpressComponentsBundleSetup-21.1.6
- [可选] RabbitMq


### 如何开始
#### 检查环境

- 已安装或配置 `RabbitMq` 
- 已安装 ` .NET 5 SDK`

#### 修改远程服务配置

1. 打开项目 `N6.Bsjc.ReportDesigner` 中的配置文件：`appsettings.json`，修改 `RemoteServices` 中的 `ReportService`下的`BaseUrl` 配置为实际的远程连接。
2. 打开项目 `N6.Bsjc.ReportDocumentViewer` 中的配置文件：`appsettings.json`，修改 `RemoteServices` 中的 `ReportService`下的`BaseUrl` 配置为实际的远程连接。

#### 运行调试项目

1. 如果在本地安装了 `RabbitMq`，`appsettings.json` 配置文件可以不修改
2. 如果本地环境没有安装相应服务，修改 `appsettings.json` 配置文件，调整相应配置节
   - 示例
   
```JSON
{
  "RabbitMQ": {
    "Connections": {
      "Default": {
        "HostName": "10.10.10.23",
        "Port": "5672",
        "UserName": "admin",
        "Password": "123456"
      }
    },
    "EventBus": {
      "ClientName": "BJJS",
      "ExchangeName": "BJJS.Exchange"
    }
  },
  "Redis": {
    "IsEnabled": "true",
    "Configuration": "10.10.10.23"
  }
}
```
3. 如果使用 Docker 创建开发环境 `docker-compose -f docker-compose.dev.yml up -d`，则使用以下开发配置 `appsettings.Development.json`
```
{
    "ConnectionStrings": {
        "Default": "Data Source=127.0.0.1;Initial Catalog=N6BSJC;UID=sa;PWD=Y9X5l6UkFOt817p1",
        "Hangfire": "127.0.0.1"
    },
    "RabbitMQ": {
        "Connections": {
            "Default": {
                "HostName": "127.0.0.1",
                "Port": "5672",
                "UserName": "bsjc",
                "Password": "bsjc"
            }
        },
        "EventBus": {
            "ClientName": "BJJS",
            "ExchangeName": "BJJS.Exchange"
        }
    },
    "Redis": {
        "IsEnabled": "true",
        "Configuration": "127.0.0.1"
    },
    "Blob": {
        "Mode": "Minio",
        "Path": "Files", //使用本地路径保存时
        "Minio": {
            "EndPoint": "127.0.0.1:9022", //不要使用 http 开头，端口不要使用控制台管理端口
            "AccessKey": "minioadmin",
            "SecretKey": "minioadmin",
            "BucketName": "bsjc", //小写
            "Url": "http://127.0.0.1:9022/bsjc/host/"
        }
    }
}
```

### 开发规范

- 编码规范
  - 项目代码风格统一使用 .editorconfig 配置文件进行约定

### 源代码管理规范

#### 规则要求

- 根据功能和问题创建单独的分支进行开发和修复
    - 请不要把多个功能和问题混在一个分支上开发提交
- 尽可能按最小功能点进行代码提交
- 提交前进行检查
    - 检查提交的文件是否和本次开发的功能点相关
    - 提交时去掉不相关的文件
- 已开发完成的分支及时提交推送和发起合并请求
  - 不及时提交容易造成冲突

#### 分支策略

- matser	目前master分支就是默认的开发分支
   
#### 流程

**一般功能开发和问题修复**

1. 基于 master 分支创建一个 feature 或 bugfix 分支
1. 开发完成后，申请合并到 master 分支

**线上问题修复**

1. 在相应版本 master 分支下（通过版本标签），创建 hotfix 分支
1. 开发测试完成后，合并 master 分支

**版本发布**


#### 分支命名要求

- 分支命名规则：分支前缀-禅道编号
    - feature-123、bugfix-123、hotfix-123
- 如果没有禅道编号：禅道编号使用任务的简要说明方字代替

#### 提交消息格式

```类型#ID 提交说明```
* 类型
  * task
  * bug
* ID
  * 禅道任务ID
  * 禅道BugID
* 提交说明
  * 长度不要超过50个文字
  * 超长的文字换行输入
* 示例：
  * ```task#1234 增加登录功能```
  * ```bug#567 修复无法打印的问题```

#### 如何发起合并请求
1. 在 GitLab 网页上为推送的分支创建一个合并请求，并选择评审人员（http://192.168.2.20/n6/backend/n6.bsjc/-/merge_requests）
    1. 源分支选择当前推送的分支
    2. 目标分支选择 dev 分支
    3. 选择审核人员
    4. 提交给评审人员评审
2. 如果评审被拒绝，修改代码后重新推送，并重新发起合并请求
3. 评审通过后，删除此次开发的分支，同时拉取最新的 dev 分支代码（远程仓库相应分支在合并后会自动删除）

### 参考资料

- [官方文档](https://docs.abp.io/zh-Hans/abp/latest)

