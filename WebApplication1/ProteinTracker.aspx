<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProteinTracker.aspx.cs" Inherits="ProteinTrackerWebServices.ProteinTracker" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Protein Tracker</title>
    <script type="text/javascript" src="Scripts/jquery-2.0.2.js"></script>
    <script type="text/javascript">

        var _users;

        $(document).ready(function () {
            $("#select-user").change(function() {
                UpdateUserData();
            });

            PopulateSelectUsers();
        });

        function PopulateSelectUsers() {
            var selectUser = $('#select-user');
            selectUser.empty();
            PageMethods.ListUsers(function (users) {
                _users = users;
                for (var i = 0; i < users.length; i++) {
                    selectUser.append($("<option></option>")
                        .attr("value", users[i].UserId)
                        .text(users[i].Name));
                }

                UpdateUserData();
            });
        }

        function AddNewUser() {
            var name = $("#name").val();
            var goal = $("#goal").val();

            $.ajax({
                type: "POST",
                url: "ProteinTracker.aspx/AddUser",
                data: JSON.stringify({ 'name': name, 'goal': goal }),
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                success: PopulateSelectUsers
            });

            //PageMethods.AddUser(name, goal, function () {
            //    PopulateSelectUsers();
            //});
        }
        
        function AddProtein() {
            var amount = $("#amount").val();
            var userId = $("#select-user").val();
            PageMethods.AddProtein(amount, userId, function (newTotal) {
                for (var i = 0; i < _users.length; i++) {
                    if (_users[i].UserId == userId)
                        _users[i].Total = newTotal;
                }
                UpdateUserData();
            });
        }
        
        function UpdateUserData() {
            var index = $("#select-user")[0].selectedIndex;
            if (index < 0)
                return;
            $("#user-goal").text(_users[index].Goal);
            $("#user-total").text(_users[index].Total);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" EnablePageMethods="True">
        </asp:ScriptManager>
        <h1>Protein Tracker</h1>
        <div>
            <label for="select-user">Select a user</label>
            <select id="select-user"></select>
        </div>
        <hr />
        <div>
            <h2>Add new user</h2>
            <br />
            <label for="name">Name</label>
            <input id="name" type="text" /><br />
            <label for="goal">Goal</label>
            <input id="goal" type="text" /><br />
            <input id="add-new-user-button" type="button" onclick="AddNewUser()" value="Add" />
        </div>
        <hr />
        <div>
            <h2>Add protein</h2>
            <label for="amount">Amount</label>
            <input id="amount" type="text" /><br />
            <input id="add-button" type="button" onclick="AddProtein()" value="Add" />
        </div>
        <div>
            Total: <span id="user-total"></span>
            <br />
            Goal: <span id="user-goal"></span>
        </div>
    </form>
</body>
</html>
