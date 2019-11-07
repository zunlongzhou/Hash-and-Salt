<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="LoginSecurity.Register" %>

<!DOCTYPE html>
<!--
    注册页面
    为用户提供注册功能
    并且保证用户名不重名（因为Salt值我没有设定ID而是与每一个用户的用户名绑定）
    保证密码长度大于8且包含数字和字母
    保证邮箱格式正确
    底部有验证码，保证验证码输入正确

    以上条件均满足则注册成功，系统给该用户分配一个随机的八位字符串（大小写字母数字混合）
    然后将该用户名、（密码+Salt）的哈希值、Salt三个值存入数据库

-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>注册</title>
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
    <h1 class="h3 mb-3 font-weight-normal">注册</h1>
      <p>请简要填写信息进行注册</p>
  </div>
  <div class="form-label-group">
      <asp:Label ID="Label1" runat="server" Text="用户名"></asp:Label>
      <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control input" TextMode="SingleLine"></asp:TextBox>
  </div>

  <div class="form-label-group">
      <asp:Label ID="Label5" runat="server" Text="邮箱"></asp:Label>
      <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control input" TextMode="SingleLine"></asp:TextBox>
  </div>

  <div class="form-label-group">
    <asp:Label ID="Label2" runat="server" Text="密码"></asp:Label>
      <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control input" TextMode="Password"></asp:TextBox>
  </div>
  <div class="form-label-group">
    <asp:Label ID="Label4" runat="server" Text="确认密码"></asp:Label>
      <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control input" TextMode="Password"></asp:TextBox>
  </div>

        <div>
                            <asp:Label ID="calCode" runat="server" Text="验证码"></asp:Label>
                            <asp:ImageButton ID="vcImg" runat="server" ImageUrl="~/ValidateCode.aspx" OnClientClick="imag" Height="30px" Width="90px"/>

        <asp:TextBox ID="txtValidatedcode" runat="server" CssClass="form-control input" TextMode="SingleLine"></asp:TextBox>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <h4><asp:Label ID="Label3" runat="server"></asp:Label></h4>
                <h4><asp:Label ID="Label6" runat="server"></asp:Label></h4>
            </ContentTemplate>
        </asp:UpdatePanel>
   <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="注册" CssClass="btn btn-lg btn-primary btn-block"/>
  <p class="mt-5 mb-3 text-muted text-center">&copy; 2019年11月</p>
</form>
</body>
</html>