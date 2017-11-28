$(document).ready(function() {
    if (typeof $.fn.viewportChecker === 'function'){
        $('#work .block').addClass("hidden-block").viewportChecker({
            classToAdd: 'visible-block',
            offset: 100
        });
    }
	//tooltip 
	$('.form-content .item .info').each(function(){
		$(this).mouseenter(function(){
			$('.tooltip', this).show();
		}).mouseout(function(){
			$('.tooltip', this).hide();
		});
	});
	//Popup scroll
	$('#popup_preview').each(function() {
		$(window).load(function(){
			$("#popup_preview .content").mCustomScrollbar();
		});
	});
	$('#popup_big').each(function() {
		$(window).load(function(){
			$("#popup_big .conteiner").mCustomScrollbar();
		});
	});
	//Carusel
    var owl = $('#customers ul');
		owl.owlCarousel({
	    	items:1,
			loop:true,
			nav:true, 
			dots:true,
			autoplay:true,
			margin:200,
		});
    var owl2 = $('#syndication ul');
	if($(window).width() < 600 ){
		owl2.owlCarousel({
	    	items:1,
			loop:true,
			nav:false, 
			dots:true,
			autoplay:true,
			margin:100,
		});
	}else{
		owl2.on('destroy.owl2.carousel');
	}
	
	
	//menus
		if($(window).width() > 800){
			$('.user-login-menu').hover(function(){
				$(this).addClass('active');
			},function(){
				$(this).removeClass('active');
			});
		}
		$('nav .mobile-menu').click(function(){
			$('nav .bg').show(0);
			$('nav .slide-menu').animate({right:0},500);
		
		});
		$('nav .bg').click(function(){
			$('nav .bg').hide(0);
			$('nav .slide-menu').animate({right:'-315px'},500);
		
		});
	//Popup
		function popupPos(){
        	var w = $(window).height(),
				p = $('#popup').height()+144,
				pm = (w-p) / 2;
			if((w-40) > p){
				$('#popup').css({'top':pm, 'position': 'fixed'});
			}else{
				$('#popup').css({'top':'20px', 'position': 'absolute'});
			}
		}
		
		$('#popup').each(function() {
			$('.add_card').click(function(){
				$('#popup').show();
				$('#popup_bg').show();
				return false;
			});
			$('#popup .cancel, #popup .close').click(function(){
				$('#popup').hide();
				$('#popup_bg').hide();
				return false;
			});
			popupPos();
			$(window).resize(function(){
				popupPos();
			});
        });
		
		
	//FAQ page
		$('.faq .title').click(function(){
			if($(this).parent().is('.active')){
			$(this).parent().removeClass('active');
			$(this).next().slideUp(300);
			}else{
			$('.faq').removeClass('active');
			$('.faq .text').slideUp(300);
			$(this).parent().addClass('active');
			$(this).next().slideDown(300);
			}
			return false;
		});
		
		
	$('.flyers-content').each(function() {
		if($(window).width() > 800){
			function HW(){
				$('.flyers-content').css({'min-height':$(window).height() - 183});
			}
			HW();
			$(window).resize(function(){
				HW();
			});
		}
    });
	$('.file-multiple').each(function() {
		$('input', this).change(function () {
    		$(this).next('.text').html(this.files.length + " file(s) selected");
		});
	  });
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
			$('.text', pa).html('Add My Photo<br><a href="">browse</a>');
		});
		$('input', this).change(function () {
    		$(this).next('.text').html(this.files.length + " file selected");
			$(this).parent().addClass('change');
			var $input = $(this);
			var inputFiles = this.files;
			if(inputFiles == undefined || inputFiles.length == 0) return;
			var inputFile = inputFiles[0];
			var img = $('<img/>',{'class':'preview'});
			img.appendTo($(this).parent())
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
	  
	  
	  $('.picture').each(function() {
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
    		$(this).next('.text').html(this.files.length + " file selected");
			$(this).parent().addClass('change');
			var $input = $(this);
			var inputFiles = this.files;
			if(inputFiles == undefined || inputFiles.length == 0) return;
			var inputFile = inputFiles[0];
			var img = $('<img/>',{'class':'preview'});
			img.appendTo($(this).parent())
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
	 /// CreateFlyer
	 
	  $('#slider-range').each(function() {
			function addSpaces(nStr){
				nStr += '';
				x = nStr.split('.');
				x1 = x[0];
				x2 = x.length > 1 ? '.' + x[1] : '';
				var rgx = /(\d+)(\d{3})/;
				while (rgx.test(x1)) {
					x1 = x1.replace(rgx, '$1' + ' ' + '$2');
				}
				return x1 + x2;
			}
	  $( "#slider-range" ).slider({
		  range: true,
		  min: 0,
		  max: 200000,
		  values: [ 0, 150000 ],
		  slide: function( event, ui ) {
			$( "#min" ).val( "$" + ui.values[0]);
			$( "#max" ).val( "$" + ui.values[1]);
			var slidemar = ui.values[1] - ui.values[0];
			var marg = 0
			if(slidemar < 45000){
				 marg = (42 - (slidemar/1000));
				if(marg < 25){
					marg = marg + 2;
				}
			}
			$('.ui-slider-handle:first .ui-slider-tooltip').css({'margin-right':marg,'margin-left': '-'+marg+'px' }).text('$'+addSpaces(ui.values[0]));
			$('.ui-slider-handle:last .ui-slider-tooltip').css({'margin-left':marg}).text('$'+addSpaces(ui.values[1]));
		},
		create: function(event, ui) {
			var tooltip = $('<div class="ui-slider-tooltip" />').css({
				position: 'absolute',
				bottom: -48,
				left: -38
			});
			$(event.target).find('.ui-slider-handle').append(tooltip);
		},
    	});
        $('.ui-slider-handle:first .ui-slider-tooltip').text('$'+addSpaces($('#min').val()));
		$('.ui-slider-handle:last .ui-slider-tooltip').text('$'+addSpaces($('#max').val()));
    });
	$('.flyer-diz').each(function(){
		$('.block', this).hover(function(){
			$(this).toggleClass('hover');
		});
	});
	$('.flyers-blocks').each(function(){
		if($(window).width() > 800){
		function fblocks(){
			if($(window).height() > 680){
				var height = ($(window).height() - 693)/2;
				$('.flyers-blocks').css({'margin-top': height});
			}
		}
		fblocks();
		$(window).resize(function(){fblocks();});
		}
	});
	
	$('input[type="checkbox"]').each(function(){
		var box = $(this),
			id = box.attr('id'),
			label = box.next().attr('for');
		if($(this).parent('.chekbox').length){}else{
		if(label !== id){
			box.after('<label for="'+id+'"></label>');
		}
		}
	});
	$('.zillow-block').each(function() {
        $('.zillow, .zillowinput', this).click(function(){
			$('.zillow-block input').css({'color':'#ffffff'});
			$('.zillow-block .grey-right-button').addClass('active');
			setTimeout(function(){$('.zillow-block input').val('Done').css({
					'border-color':'#f46a02',
					'background-color':'#f46a02', 
					'color':'#ffffff', 
					'z-index':3
				});}, 1500);
			return false;
		});
    });
	
	//Menu fixed
	$(window).bind('scroll', function() {
		if ($(window).scrollTop() > 100) {
			$('header').addClass('fixed');
		}else{
			$('header').removeClass('fixed');
		}
	});
});