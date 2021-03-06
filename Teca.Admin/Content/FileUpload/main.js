/*
 * jQuery File Upload Plugin JS Example 6.5.1
 * https://github.com/blueimp/jQuery-File-Upload
 *
 * Copyright 2010, Sebastian Tschan
 * https://blueimp.net
 *
 * Licensed under the MIT license:
 * http://www.opensource.org/licenses/MIT
 */

/*jslint nomen: true, unparam: true, regexp: true */
/*global $, window, document */

$(function () {
    'use strict';

    // Initialize the jQuery File Upload widget:
    $('#fileupload').fileupload();
    // 50 MB
    $('#fileupload').fileupload('option', {
        maxFileSize: 10485760,
        acceptFileTypes: /(\.|\/)(mp4|webm)$/i,
            resizeMaxWidth: 1920,
            resizeMaxHeight: 1200
        });
});
