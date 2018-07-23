(function () {
    $(function () {
        var _$usersTable = $('#users-grid');
        var _userService = abp.services.app.userAppServicecs;
        var sendobj = {}
        sendobj.Sorting = "Name";
        sendobj.SkipCount = 0;
        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Admin/Users/CreateOrEditModal',
            scriptUrl: abp.appPath + 'Modules/Admin/Views/Users/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditUserModal'
        });
        
        var dataSource = new kendo.data.DataSource({
            transport: {
                read: {
                    type: "post",
                    url: "/api/services/app/userAppServicecs/getUsers",
                    dataType: "json",
                    contentType: "application/json" },
                    parameterMap: function (options, operation) {
                    if (operation == "read") {
                        var parameter = {
                            page: options.page,
                            pageSize: options.pageSize,
                            SkipCount:0,
                            Sorting:"Name"
                        };
                        return kendo.stringify(parameter);
                    }
                }
            },         
            batch: true,
            pageSize: 10,
            schema: {
                data: function (d) {
                    return d.result.items;
                },
                total: function (d) {
                    return d.result.totalCount;
                }
            },
            serverPaging: true,
            serverFiltering: true,
            serverSorting: true
        });

        _$usersTable.kendoGrid({
            dataSource: dataSource,
            columns: [
                {
                    field: "id",
                    title: " ",
                    width: 50,
                    headerAttributes: { style: "text-align:center" },
                    attributes: { style: "text-align:center" },
                    template: '<a class="btn btn-default"  href="Edit/#=id#"><i class="fa fa-pencil"></i>编辑</a>'
                },
                { field: "userName", title: "用户名",groupable: false },
                { field: "name",title: "姓名" }, 
                { field: "emailConfirm", title: "邮箱" },
                { field: "lastLoginTime", title: "最后登录时间", format: "{0: yyyy-MM-dd HH:mm:ss}" },
                { field: "creationTime", title: "创建时间", format: "{0: yyyy-MM-dd HH:mm:ss}"}

            ],
            scrollable: false,
            pageable: {
                refresh: true,
                pageSizes: true
          
            }
        });
        $("#Create").click(function () {
            _createOrEditModal.open();
        })
    })
})();