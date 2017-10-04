# A Simple Blog

AspNetCore2.0 Razor Pages Web App


Blog.Admin 用户名admin@qq.com 密码可能是123qwe或者1234qwer

# 注意事项

- 开发时用的是LocalDb,在提交github前改用sqlite,由于SQLServer数据类型与sqlite冲突,于是新建一个迁移`Blog.Core\MigrationsForSqlite`
- 两个迁移无法同时存在一个项目,要在Visual Studio中手动排除文件夹来切换
- sqlite使用的数据库已经生成,在`src/app.db`,由于是两个WebApp所以发布时(并且打算使用sqlite)要注意修改ConnectionString







