<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProteinTracker.aspx.cs" Inherits="ProteinTracker.ProteinTracker" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Protein Tracker</title>
    <script type="text/javascript" src="Scripts/jquery-3.3.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            PopulateSelectUsers();
        })

        function PopulateSelectUsers() {
            var selectUser = $('#select-user');
            selectUser.empty();
            ProteinTracker.ProteinTrackerWebservice.ListUsers(function (users) {
                for (var i = 0; i < users.length; i++) {
                    selectUser.append($ < option ></option >)
                        .attr("value", users[i].UserId)
                        .text(users[i].Name)
                    )
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server">
            <Services>
                <asp:ServiceReference Path="~/ProteinTrackerWebservice.asmx.cs" />
            </Services>
        </asp:ScriptManager>
        <h1>Protein Tracker</h1>
        <div>
            <label for="select-user">Select a user</label>
            <select id="select-user"></select>
        </div>
    </form>
</body>
</html>
