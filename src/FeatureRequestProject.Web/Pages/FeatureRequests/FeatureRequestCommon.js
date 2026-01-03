var FeatureRequestCommon = (function ($) {
    return {
        createFilterColumn: function (l, title, dataField, items, onSelect) {
            var filterClass = 'filter-' + dataField;

            var generateHtml = function () {
                var listHtml = `<li><a class="dropdown-item ${filterClass}" href="#" data-val="">${l('All')}</a></li>`;

                if (items) {
                    items.forEach(i => {
                        listHtml += `<li><a class="dropdown-item ${filterClass}" href="#" data-val="${i.id}">${i.name}</a></li>`;
                    });
                }

                return `
                <div class="dropdown">
                    <button class="btn btn-sm bg-transparent border-0 fw-bold text-start text-uppercase text-secondary dropdown-toggle" 
                            type="button" 
                            id="btn-${dataField}"
                            data-bs-toggle="dropdown" 
                            data-bs-boundary="window"
                            data-bs-popper-config='{"strategy":"fixed"}'
                            aria-expanded="false"
                            style="box-shadow: none;">
                        <i class="fa fa-filter me-1"></i> <span id="lbl-${dataField}">${title}</span>
                    </button>
                    <ul class="dropdown-menu shadow">
                        ${listHtml}
                    </ul>
                </div>`;
            };

            $(document).off('click', '.' + filterClass).on('click', '.' + filterClass, function (e) {
                e.preventDefault();
                var val = $(this).data('val');
                var text = $(this).text();

                var $label = $('#lbl-' + dataField);
                var $btn = $('#btn-' + dataField);

                if (val === "" || val === null || val === undefined) {
                    $label.text(title);
                    $btn.removeClass('text-primary').addClass('text-secondary');
                    val = null;
                } else {
                    $label.text(text);
                    $btn.removeClass('text-secondary').addClass('text-primary');
                }

                onSelect(val);
            });

            return {
                title: generateHtml(),
                data: dataField,
                orderable: false,
                className: "th-filter-column",
                render: function (data) {
                    return l('Enum:' + title + '.' + data);
                }
            };
        }
    };
})(jQuery);