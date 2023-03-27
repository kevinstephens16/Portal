var profileListVM;
$(function () {
    profileListVM = {
        dt: null,

        init: function () {
            dt = $('#profile-data-table').DataTable({
                "serverSide": true,
                "processing": true,
                "ajax": {
                    "url": "@Url.Action('GetProfile','Profile')",
                "data": function (data) {

                }
            },
                "columns": [
                    { "title": "User", "data": "UserName", "searchable": true },
                    { "title": "First Name", "data": "FirstName", "searchable": true },
                    { "title": "Last Name", "data": "LastName", "searchable": true },
                    { "title": "Title", "data": "Title", "searchable": true },
                    { "title": "Phone", "data": "Phone", "searchable": true },
                    { "title": "Alt Phone", "data": "AltPhone", "searchable": true },
                    { "title": "Fax", "data": "Fax", "searchable": true },
                    { "title": "E-mail", "data": "Email", "searchable": true },
                    { "title": "Quality Document", "data": "QualityDoc", "searchable": true },
                    {
                        "title": "Actions",
                        "data": "IDUrl",
                        "searchable": false,
                        "sortable": false,
                        "render": function (data, type, full, meta) {
                            return '<a href="@Url.Action("EditProfile","Profile")?id=' + data + '" class="glyphicon glyphicon-pencil editProfile"></a>';
                        }
                    }
                ],
                "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]],
                });
},

    refresh: function () {
        dt.ajax.reload();
    }
}

// initialize the datatables
profileListVM.init();

$('#profile-data-table').on("click", ".editProfile", function (event) {
    event.preventDefault();
    var url = $(this).attr("href");
    $.get(url, function (data) {
        $('#editProfileContainer').html(data);
        $('#editProfileModal').modal('show');
    });
});
});

/**** Edit Profile Ajax Form CallBack ********/
function UpdateProfileSuccess(data) {
    if (data !== "success") {
        $('#editProfileContainer').html(data);
        return;
    }
    $('#editProfileModal').modal('hide');
    $('#editProfileContainer').html("");
    profileListVM.refresh();
}