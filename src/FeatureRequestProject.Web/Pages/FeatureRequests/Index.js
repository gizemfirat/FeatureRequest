$(function () {
    var l = abp.localization.getResource('FeatureRequestProject');
    var createModal = new abp.ModalManager(abp.appPath + 'FeatureRequests/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'FeatureRequests/EditModal');

    var dataTable = $('#FeatureRequestsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, 'asc']],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(featureRequestProject.featureRequests.featureRequest.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    confirmMessage: function (data) {
                                        return l('FeatureRequestDeletionConfirmationMessage', data.record.title);
                                    },
                                    action: function (data) {
                                        featureRequestProject.featureRequests.featureRequest
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(l('SuccessfullyDeleted'));
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('Title'),
                    data: "title"
                },
                {
                    title: l('Description'),
                    data: "description"
                },
                {
                    title: l('Category'),
                    data: "categoryId",
                    render: function (data) {
                        return l('Enum:Category.' + data);
                    }
                },
                {
                    title: l('Status'),
                    data: "status",
                    render: function (data) {
                        return l('Enum:Status.' + data);
                    }
                }
                //TODO : Add more columns if needed
            ]
        })
    );

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewFeatureRequestButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});