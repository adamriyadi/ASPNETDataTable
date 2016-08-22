$(document).ready(function () {
    $('#example').DataTable({
        destroy: true,
        processing: true,
        serverSide: true,
        ajax: {
            url: api_url + 'orders',
            type: 'POST'
        },
        columns: [
            { data: 'firstname' },
            { data: 'lastname' },
            { data: 'productname' },
            { data: 'qty', render: render_number },
            { data: 'totalprice', render: render_number }
        ],
        order: [[0, 'asc']]
    });
});

function render_number(data, type, row, meta) {
    return numeral(data).format('#,##0');
}