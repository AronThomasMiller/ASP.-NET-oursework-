(function ($) {
    $(".edit-user-btn").on("click", function (e) {
        var user = $(e.currentTarget).data("element");
        var dropdown = $(e.currentTarget).closest("tr").find("#Role").val();
        var url = $("#EditUserUrl").val();

        $.ajax({
            type: "GET",
            url: url,
            data: {
                userId: user.UserId,
                roleId: dropdown,
                oldRoleName: user.RoleName
            },
            success: function () {
                location.reload();

            }
        });
    });
} (jQuery))