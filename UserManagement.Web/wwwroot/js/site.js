$(document).ready(function () {
    let pagerOptions = {
        // target the pager markup - see the HTML block below
        container: $(".pager"),
        // output string - default is '{page}/{totalPages}';
        // possible variables: {size}, {page}, {totalPages}, {filteredPages}, {startRow}, {endRow}, {filteredRows} and {totalRows}
        // also {page:input} & {startRow:input} will add a modifiable input in place of the value
        output: '{startRow} - {endRow} / {filteredRows} ({totalRows})',
        // if true, the table will remain the same height no matter how many records are displayed. The space is made up by an empty
        // table row set to a height to compensate; default is false
        fixedHeight: false,
        // remove rows from the table to speed up the sort of large tables.
        // setting this to false, only hides the non-visible rows; needed if you plan to add/remove rows with the pager enabled.
        removeRows: false,
        // go to page selector - select dropdown that sets the current page
        cssGoto: '.gotoPage'
    };

    $('.tablesorter').tablesorter({
        theme: 'blue',
        showProcessing: true,
        headerTemplate: '{content} {icon}',
        widgets: ['uitheme', 'zebra', 'resizable', 'filter', 'scroller'],
        widgetOptions: {
            scroller_height: 300,
            // scroll tbody to top after sorting
            scroller_upAfterSort: true,
            // pop table header into view while scrolling up the page
            scroller_jumpToHeader: true,
            // In tablesorter v2.19.0 the scroll bar width is auto-detected
            // add a value here to override the auto-detected setting
            scroller_barWidth: null,
        }
    }).tablesorterPager(pagerOptions);
      
});
