# COMPASS
 
<div>
 <img src="https://raw.githubusercontent.com/felixzhu1989/Compass/main/Compass/images/COMPASS.png" alt="compass">
</div>

<br/>

<div>   
  <img src="https://img.shields.io/badge/language-csharp-green.svg" alt="lang">
  <img src="https://img.shields.io/badge/Github-build-blue.svg?style=flat-square" alt="Github">
  <a href="https://github.com/felixzhu1989/Compass/releases">
    <img src="https://img.shields.io/github/release/iawia002/annie.svg?style=flat-square" alt="GitHub release">
  </a> 
</div>

<br/>

"COMPASS" 是Halton(上海)项目管理和SolidWorks自动绘图程序。

### 背景

随着FoodService和Marine事业部的发展，项目经理和项目工程师的工作任务日益增加。项目经理收发各种邮件，不能有效的将信息统一管理；项目工程师面临着繁重的制图任务和递增的出图错误率的困扰。我们开发了一套集项目管理和SolidWorks自动绘图程序，用于提高生产效率。

1. 将项目管理分为三大模块：项目信息、项目跟踪和项目统计三大模块。

2. 对产品进行分类，逐一定义每一个分类的参数化规范，最终实现自动化的参数建模。

> * 程序类型：WinForm窗口应用程序
> * 运行系统：windows7、windows10
> * 系统环境：SolidWorks 2021 SP0、.NET FrameWorks 4.5.1、Microsoft SQL Server 2014
> * 开发环境：Microsoft VisualStudio 2017、Microsoft VisualStudio 2019
> * 开发语言：C#、SQL


### 数据库

...\SQLServerDB 文件夹中有SQL脚本文件：

1. 在SQL数据库中打开CompassDB.sql文件，建立数据库；
2. 打开CompassDB_Table.sql添加表；
3. 打开CompassDB_Constraint.sql建立表间关系；
4. CompassDB_Model.sql里边是Halton的模型数据表。

修改数据库连接字符串，手动添加用户以登陆。

注意：在Halton局域网以外是没法实际作图，因为SolidWorks模型文件存储在Halton的PDM中。


### 过程记录

 https://www.bilibili.com/video/BV1ra4y1v7Mk
 
 
### 学习视频

 https://www.bilibili.com/video/BV1954y1J7iE 
 https://www.bilibili.com/video/BV1LE411K7ro
 https://www.bilibili.com/video/BV1ya4y1n7xQ
 
 
### 联系我

Email: zhu-hongfeng@hotmail.com

### 贡献者

感谢以下参与项目的人：

<a href="https://github.com/felixzhu1989/Compass/graphs/contributors"><img src="https://github.com/felixzhu1989/Compass/blob/main/Compass/images/contributors.png" /></a>

