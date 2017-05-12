var dropZ = Dropzone.options.dropzoneForm = {
    maxFiles: 1,
    maxFilesize: 160,
    init: function () {
        this.on("complete", function (file, response) {
            if (response.code == 501) { // succeeded
                return file.previewElement.classList.add("dz-success"); // from source
            }
            else {  //  error
                // below is from the source code too
                var node, _i, _len, _ref, _results;
                var message = "Can't upload, wrong file size or type"; // modify it to your error message
                file.previewElement.classList.add("dz-error");
                _ref = file.previewElement.querySelectorAll("[data-dz-errormessage]");
                _results = [];
                for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                    node = _ref[_i];
                    _results.push(node.textContent = message);
                }
                return _results;
            }
        });

        this.on("maxfilesexceeded", function (data) {
            var res = eval('(' + data.xhr.responseText + ')');
        });
        this.on("addedfile", function (file) {
            // Create the remove button
            var removeButton = Dropzone.createElement("<button>Remove file</button>");
            // Capture the Dropzone instance as closure.
            var _this = this;
            // Listen to the click event
            removeButton.addEventListener("click", function (e) {
                // Make sure the button click doesn't submit the form:
                e.preventDefault();
                e.stopPropagation();
                // Remove the file preview.
                _this.removeFile(file);
                // If you want to the delete the file on the server as well,
                // you can do the AJAX request here.
            });
            // Add the button to the file preview element.
            file.previewElement.appendChild(removeButton);
        });
    }
};