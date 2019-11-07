<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="LoginSecurity.HomePage" %>

<!--
    这是项目的入口
    页面是登陆页面，连接数据库进行验证
    验证方法是通过得到用户口令加上该用户在数据库内保存的Salt值哈希，
    再与数据库内保存的哈希值比对的 哈希加盐方法
    登陆加上了验证码，并且点击验证码图片可以刷新验证码
-->


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>登陆</title>
    <link rel="stylesheet" href="BootStrap/css/bootstrap.css" />
    <link rel="stylesheet" href="BootStrap/css/bootstrap-theme.css" />
    <link rel="stylesheet" href="BootStrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="BootStrap/floating-labels.css" />


    <script src="BootStrap/jquery.js"></script>
    <script src="BootStrap/js/bootstrap.js"></script>
    <script src="BootStrap/js/bootstrap.min.js"></script>

    <style>
      .bd-placeholder-img {
        font-size: 1.125rem;
        text-anchor: middle;
        -webkit-user-select: none;
        -moz-user-select: none;
        -ms-user-select: none;
        user-select: none;
      }

      @media (min-width: 768px) {
        .bd-placeholder-img-lg {
          font-size: 3.5rem;
        }
      }
      .input {
            width: 100%;
            height: calc(1.5em + .75rem + 2px);
            padding: .375rem .75rem;
            color: #495057;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            margin: 6px 7px 2px 4px;
            color:#28a745;
            border-color:#28a745
        }
    </style>
    
</head>
<body>
    <form class="form-signin" runat="server" id="form1">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
  <div class="text-center mb-6">
    <img class="mb-4" src="Image/cat.png" alt="" width="72" height="72" />
    <h1 class="h3 mb-3 font-weight-normal">登陆</h1>
    <p>这里是我做的 <code>NBA小站</code>希望你会喜欢<a href="#">如果有什么问题可以随时反馈</a></p>
      <p>你可以在这里输入用户名和密码进行登陆,也可以点击下方按钮注册</p>
  </div>

  <div class="form-label-group">
      <asp:Label ID="Label1" runat="server" Text="用户名"></asp:Label>
      <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control input" TextMode="SingleLine"></asp:TextBox>

  </div>

  <div class="form-label-group">
    <asp:Label ID="Label2" runat="server" Text="密码"></asp:Label>
      <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control input" TextMode="Password"></asp:TextBox>
  </div>

    <div>
                            <asp:Label ID="calCode" runat="server" Text="验证码"></asp:Label>
                            <asp:ImageButton ID="vcImg" runat="server" ImageUrl="~/ValidateCode.aspx" OnClientClick="imag" Height="30px" Width="90px"/>

        <asp:TextBox ID="txtValidatedcode" runat="server" CssClass="form-control input" TextMode="SingleLine"></asp:TextBox>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <h4><asp:Label ID="Label3" runat="server"></asp:Label></h4>
            </ContentTemplate>
        </asp:UpdatePanel>
  <div class="checkbox mb-3">
    <label>
      <input type="checkbox" value="remember-me" /> 记住我
    </label>
  </div>
        <br />
   <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="登陆" CssClass="btn btn-lg btn-primary btn-block"/>
        <br />
   <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="注册" CssClass="btn btn-lg btn-primary btn-block"/>
  <p class="mt-5 mb-3 text-muted text-center">&copy; 2019年11月</p>
</form>
</body>
</html>
