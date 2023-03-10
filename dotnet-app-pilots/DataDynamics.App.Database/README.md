# .net용 데이터베이스 테스트용 프로젝트

## 목표

* 데이터베이스 연결을 위한 App.config를 설정
* 데이터베이스에 CRUD 예제 코드 작성

## 필요한 패키지

* NUnit (PM> NuGet\Install-Package nunit)
* System Configuration Manager
* Postgres & Npgsql (https://github.com/npgsql/npgsql)
  * PM> NuGet\Install-Package Npgsql
  * PM> NuGet\Install-Package Microsoft.EntityFrameworkCore
  * PM> NuGet\Install-Package NpgsqlEntityFrameworkCore.PostgreSQL

## Postgres Portable for Windows

* Download : https://sourceforge.net/projects/pgsqlportable/
* Username : postgres
* Password : Clave:123

## Postgres ODBC Driver

개인 PC에서 ODBC를 통해 DB에 접속하려면 ODBC Driver를 설치하도록 합니다.

* Download : https://ftp.postgresql.org/pub/odbc/versions/msi/psqlodbc_13_02_0000-x64.zip

## App.config

`App.config` 파일은 .net에서 사용하는 표준화된 설정 파일로서 XML 형식입니다.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
	<appSettings>
		<add key="AppName" value="Database App"/>
	</appSettings>
</configuration>
```
이렇게 추가한 설정은 다음과 같이 로딩할 수 있습니다.

```csharp
string appName = ConfigurationManager.AppSettings["AppName"];
```

App Settings에 대한 상세한 내용은 https://learn.microsoft.com/en-us/dotnet/api/system.configuration.configurationmanager?view=dotnet-plat-ext-7.0 을 참고합니다.

## Entity Framework

### Npgsql

```sql
CREATE TABLE customers (
            id bigint,
            full_name text NULL,
            CONSTRAINT "PK_customers" PRIMARY KEY (id)
)

SELECT c.id, c.full_name
        FROM customers AS c
        WHERE c.full_name = 'John Doe';
```

## LINQ (Language-Integrated Query)

## 참고사이트

* Npgsql (https://www.npgsql.org/efcore/index.html)