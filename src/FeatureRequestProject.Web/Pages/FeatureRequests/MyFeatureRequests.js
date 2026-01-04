$(function () {
    var l = abp.localization.getResource('FeatureRequestProject');
    var createModal = new abp.ModalManager(abp.appPath + 'FeatureRequests/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'FeatureRequests/EditModal');
    var viewModal = new abp.ModalManager(abp.appPath + 'FeatureRequests/ViewModal');

    var catList = typeof categoryListFromServer !== 'undefined' ? categoryListFromServer : [];
    var statList = typeof statusListFromServer !== 'undefined' ? statusListFromServer : [];

    var filters = {
        category: null,
        status: null,
        isMyRequests: true
    };

    var dataTable = $('#MyFeatureRequestsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, 'asc']],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(
                featureRequestProject.featureRequests.featureRequest.getList,
                function () { return filters; }
            ),
            columnDefs: [
                {
                    title: "",
                    width: "30px",
                    orderable: false,
                    render: function (data, type, row) {
                        return `<i class="fa fa-info-circle text-primary view-detail-icon" style="cursor:pointer" data-id="${row.id}"></i>`;
                    }
                },
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
                { title: l('Title'), data: "title" },
                { title: l('Votes'), data: "voteCount", className: "text-start dt-head-left" },
                FeatureRequestCommon.createFilterColumn(l, 'Category', 'categoryId', catList, function (selectedVal) {
                    filters.category = selectedVal;
                    dataTable.ajax.reload();
                }),

                FeatureRequestCommon.createFilterColumn(l, 'Status', 'status', statList, function (selectedVal) {
                    filters.status = selectedVal;
                    dataTable.ajax.reload();
                })
            ],
        })
    );

    createModal.onResult(function () { dataTable.ajax.reload(); });
    editModal.onResult(function () { dataTable.ajax.reload(); });

    $('#NewFeatureRequestButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    $(document).on('click', '.view-detail-icon', function () {
        var id = $(this).attr('data-id');
        viewModal.open({ id: id });
    });
});