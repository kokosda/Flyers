$(document).ready(function() {
		$('.file-one').each(function() {
		function something_happens() {
			input.replaceWith(input.val('').clone(true));
		};
		$('input.delete', this).on('click', function(){
			var control = $(this).parent().children('input[type="file"]');
			var pa = $(this).parent();
			control.replaceWith( control.val('').clone( true ) );
			pa.children().remove('img');
			pa.removeClass('change');
		});
		$('input', this).change(function () {
			$(this).parent().parent().addClass('change');
			var $input = $(this);
			var inputFiles = this.files;
			if(inputFiles == undefined || inputFiles.length == 0) return;
			var inputFile = inputFiles[0];
			var img = $('<img/>',{'class':'preview'});
			img.appendTo($(this).parent().parent())
			var reader = new FileReader();
			reader.onload = function(event) {
				img.attr("src", event.target.result);
			};
			reader.onerror = function(event) {
				alert("I AM ERROR: " + event.target.error.code);
			};
			reader.readAsDataURL(inputFile);
		});
	  });
	  $('.upload').each(function() {
		$('input', this).change(function () {
			var $this = $(this),
     			 $val = $this.val(),
      			valArray = $val.split('\\'),
      			newVal = valArray[valArray.length-1];
			$(this).next('.file-text').text(newVal);
		});
	  });
	 
});