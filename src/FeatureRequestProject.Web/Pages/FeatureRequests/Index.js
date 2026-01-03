$(function () {
    var l = abp.localization.getResource('FeatureRequestProject');
    var createModal = new abp.ModalManager(abp.appPath + 'FeatureRequests/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'FeatureRequests/EditModal');
    var viewModal = new abp.ModalManager(abp.appPath + 'FeatureRequests/ViewModal');

    var filters = { category: null, status: null };

    var catList = typeof categoryListFromServer !== 'undefined' ? categoryListFromServer : [];
    var statList = typeof statusListFromServer !== 'undefined' ? statusListFromServer : [];

    var dataTable = $('#FeatureRequestsTable').DataTable(
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
                    visible: abp.auth.isGranted('FeatureRequestProject.FeatureRequests.Edit') || abp.auth.isGranted('FeatureRequestProject.FeatureRequests.Delete'),
                    rowAction: {
                        items: [
                            {
                                text: l('Edit'),
                                visible: abp.auth.isGranted('FeatureRequestProject.FeatureRequests.Edit'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('FeatureRequestProject.FeatureRequests.Delete'),
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
                    title: l('Creator'),
                    data: "creatorUserName",
                    render: function (data) {
                        if (!data) return 'System';
                        return '<span class="badge bg-secondary">' + data + '</span>';
                    }
                },
                {
                    title: l('Votes'),
                    data: "voteCount"
                },
                {
                    title: l('Description'),
                    data: "description"
                },

                FeatureRequestCommon.createFilterColumn(l, 'Category', 'categoryId', catList, function (selectedVal) {
                    filters.category = selectedVal;
                    dataTable.ajax.reload();
                }),
                FeatureRequestCommon.createFilterColumn(l, 'Status', 'status', statList, function (selectedVal) {
                    filters.status = selectedVal;
                    dataTable.ajax.reload();
                })
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

    $(document).on('click', '.view-detail-icon', function () {
        var id = $(this).attr('data-id');
        viewModal.open({ id: id });
    });

    $(document).on('click', '.modal-vote-btn', function (e) {
        var id = $(this).attr('data-id');
        var type = $(this).attr('data-type');

        featureRequestProject.featureRequests.featureRequest.vote(id, type)
            .then(function () {
                abp.notify.success(l('VoteSaved'));
                dataTable.ajax.reload();
                viewModal.close();
            });
    });

    $(document).on('click', '#btn-send-comment', function (e) {
        var $btn = $(this);
        var id = $btn.data('id');
        var content = $('#NewCommentContent').val();

        if (!content) {
            abp.notify.warn(l('CommentCannotBeEmpty'));
            return;
        }

        $btn.prop('disabled', true);

        featureRequestProject.featureRequests.featureRequest.createComment(id, content)
            .then(function () {
                abp.notify.success(l('CommentSaved'));
                var url = abp.appPath + 'FeatureRequests/ViewModal?id=' + id;

                $.get(url, function (response) {
                    var $responseHtml = $(response);
                    var newCommentsHtml = $responseHtml.find('#comments-section').html();
                    $('#comments-section').html(newCommentsHtml);

                    $('#NewCommentContent').val('');
                }).always(function () {
                    $btn.prop('disabled', false);
                });

            })
            .catch(function (err) {
                $btn.prop('disabled', false);
            });
    });
});