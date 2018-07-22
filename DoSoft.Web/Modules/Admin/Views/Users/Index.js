(function () {
    $(function () {
        var _$usersTable = $('#users-grid');
        var _userService = abp.services.app.userAppServicecs;
        var data = _userService.getUsers();
        console.log(data);
        var dataSource = new kendo.data.DataSource({
            transport: {
                data: data,
                parameterMap: function (options, operation) {
                    if (operation == "read") {
                        var parameter = {
                            page: options.page,
                            pageSize: options.pageSize,

                        };
                        return kendo.stringify(parameter);
                    }
                }
            },
            batch: true,
            pageSize: 10,
            schema: {
                data: function (d) {
                    return d.data;
                },
                total: function (d) {
                    return d.total;
                }
            },
            serverPaging: true
        });

        _$usersTable.kendoGrid({
            dataSource: dataSource,
            columns: [
                { field: "UserName", groupable: false },
                { field: "Name" }, /* group by this column to see the footer template */
                { field: "EmailConfirm" },
                { field: "LastLoginTime" },
                { field: "CreationTime" }

            ],
            groupable: true,
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            }
        });

    })
})();