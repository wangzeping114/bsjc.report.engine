### ��Ŀ�ֲ�

```
- N6.Bsjc.ReportDesigner  ���������վ��
- N6.Bsjc.ReportDocumentViewer ����鿴��վ��
- N6.Bsjc.Reporting.Domain ���������(�ṩ����鿴���ͱ����������֧��)
- N6.Bsjc.Reporting.Domain.Shared �����������(����ͨ�õ�DTO��ö��)  
- N6.Bsjc.Reporting.Domain.Test ����㵥Ԫ����
- N6.Bsjc.Reporting.HttpApi.Client �������ݷ��ʲ�(Զ�̽ӿڵ���)
- N6.Bsjc.Reporting.HttpApi.Client.Contracts  �������ݷ��ʲ���Լ(���������ͽӿ�,DTO,ö��)
- N6.Core.Abp.Client.Contracts ��ܲ���Լ�����ݷ��ʲ�(�ṩͨ�õ�Զ�̷��ʷ�ʽ,Ŀǰ���õ�Refit)
- N6.Core.Abp.Client.HttpApi ��ܲ���Լ��ʵ�ֲ� (�ṩͨ�õ�Զ�̷��ʷ�ʽ,Ŀǰ���õ�Refit)
```
### ��������

- [����] .NET 5 
- [����] Visual Studio 2019 / VS Code
- [����] DevExpressComponentsBundleSetup-21.1.6
- [��ѡ] RabbitMq


### ��ο�ʼ
#### ��黷��

- �Ѱ�װ������ `RabbitMq` 
- �Ѱ�װ ` .NET 5 SDK`

#### �޸�Զ�̷�������

1. ����Ŀ `N6.Bsjc.ReportDesigner` �е������ļ���`appsettings.json`���޸� `RemoteServices` �е� `ReportService`�µ�`BaseUrl` ����Ϊʵ�ʵ�Զ�����ӡ�
2. ����Ŀ `N6.Bsjc.ReportDocumentViewer` �е������ļ���`appsettings.json`���޸� `RemoteServices` �е� `ReportService`�µ�`BaseUrl` ����Ϊʵ�ʵ�Զ�����ӡ�

#### ���е�����Ŀ

1. ����ڱ��ذ�װ�� `RabbitMq`��`appsettings.json` �����ļ����Բ��޸�
2. ������ػ���û�а�װ��Ӧ�����޸� `appsettings.json` �����ļ���������Ӧ���ý�
   - ʾ��
   
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
3. ���ʹ�� Docker ������������ `docker-compose -f docker-compose.dev.yml up -d`����ʹ�����¿������� `appsettings.Development.json`
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
        "Path": "Files", //ʹ�ñ���·������ʱ
        "Minio": {
            "EndPoint": "127.0.0.1:9022", //��Ҫʹ�� http ��ͷ���˿ڲ�Ҫʹ�ÿ���̨����˿�
            "AccessKey": "minioadmin",
            "SecretKey": "minioadmin",
            "BucketName": "bsjc", //Сд
            "Url": "http://127.0.0.1:9022/bsjc/host/"
        }
    }
}
```

### �����淶

- ����淶
  - ��Ŀ������ͳһʹ�� .editorconfig �����ļ�����Լ��

### Դ�������淶

#### ����Ҫ��

- ���ݹ��ܺ����ⴴ�������ķ�֧���п������޸�
    - �벻Ҫ�Ѷ�����ܺ��������һ����֧�Ͽ����ύ
- �����ܰ���С���ܵ���д����ύ
- �ύǰ���м��
    - ����ύ���ļ��Ƿ�ͱ��ο����Ĺ��ܵ����
    - �ύʱȥ������ص��ļ�
- �ѿ�����ɵķ�֧��ʱ�ύ���ͺͷ���ϲ�����
  - ����ʱ�ύ������ɳ�ͻ

#### ��֧����

- matser	Ŀǰmaster��֧����Ĭ�ϵĿ�����֧
   
#### ����

**һ�㹦�ܿ����������޸�**

1. ���� master ��֧����һ�� feature �� bugfix ��֧
1. ������ɺ�����ϲ��� master ��֧

**���������޸�**

1. ����Ӧ�汾 master ��֧�£�ͨ���汾��ǩ�������� hotfix ��֧
1. ����������ɺ󣬺ϲ� master ��֧

**�汾����**


#### ��֧����Ҫ��

- ��֧�������򣺷�֧ǰ׺-�������
    - feature-123��bugfix-123��hotfix-123
- ���û��������ţ��������ʹ������ļ�Ҫ˵�����ִ���

#### �ύ��Ϣ��ʽ

```����#ID �ύ˵��```
* ����
  * task
  * bug
* ID
  * ��������ID
  * ����BugID
* �ύ˵��
  * ���Ȳ�Ҫ����50������
  * ���������ֻ�������
* ʾ����
  * ```task#1234 ���ӵ�¼����```
  * ```bug#567 �޸��޷���ӡ������```

#### ��η���ϲ�����
1. �� GitLab ��ҳ��Ϊ���͵ķ�֧����һ���ϲ����󣬲�ѡ��������Ա��http://192.168.2.20/n6/backend/n6.bsjc/-/merge_requests��
    1. Դ��֧ѡ��ǰ���͵ķ�֧
    2. Ŀ���֧ѡ�� dev ��֧
    3. ѡ�������Ա
    4. �ύ��������Ա����
2. ������󱻾ܾ����޸Ĵ�����������ͣ������·���ϲ�����
3. ����ͨ����ɾ���˴ο����ķ�֧��ͬʱ��ȡ���µ� dev ��֧���루Զ�ֿ̲���Ӧ��֧�ںϲ�����Զ�ɾ����

### �ο�����

- [�ٷ��ĵ�](https://docs.abp.io/zh-Hans/abp/latest)

