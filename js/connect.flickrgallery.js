var galleryService;

function GalleryService($, settings, mid) {
	var moduleId = mid;
	var baseServicepath = $.dnnSF(moduleId).getServiceRoot('Connect/FlickrGallery');

	this.ServicePath = baseServicepath;

	this.viewCall = function (controller, action, view, id, success, fail) {
		showLoading();
		$.ajax({
			type: "GET",
			url: baseServicepath + controller + '/' + action + '/' + id,
			beforeSend: $.dnnSF(moduleId).setModuleHeaders,
			data: { view: view }
		}).done(function (data) {
			hideLoading();
			if (success != undefined) {
				success(data);
			}
		}).fail(function (xhr, status) {
			showError(xhr.responseText);
			if (fail != undefined) {
				fail(xhr.responseText);
			}
		});
	}

	this.dataCall = function (controller, action, data, success, fail) {
		showLoading();
		$.ajax({
			type: "GET",
			url: baseServicepath + controller + '/' + action,
			beforeSend: $.dnnSF(moduleId).setModuleHeaders,
			data: data
		}).done(function (retdata) {
			hideLoading();
			if (success != undefined) {
				success(retdata);
			}
		}).fail(function (xhr, status) {
			showError(xhr.responseText);
			if (fail != undefined) {
				fail(xhr.responseText);
			}
		});
	}

	this.apiPostCall = function (controller, action, data, success, fail) {
		showLoading();
		$.ajax({
			type: "POST",
			url: baseServicepath + controller + '/' + action,
			beforeSend: $.dnnSF(moduleId).setModuleHeaders,
			data: data
		}).done(function (retdata) {
			hideLoading();
			if (success != undefined) {
				success(retdata);
			}
		}).fail(function (xhr, status) {
			showError(xhr.responseText);
			if (fail != undefined) {
				fail(xhr.responseText);
			}
		});
	}

	this.nextGallerySegment = function (id, view, success, fail) {
		this.viewCall('Photos', 'Page', view, id, success, fail);
	}
}

function showLoading() {
	if ($('#cgStatus').length) {
		$('#cgStatus div:first-child').show();
		$('#cgStatus div:nth-child(2)').hide();
		$('#cgStatus').css('background', '#2FC1F3').show();
	}
}

function hideLoading() {
	if ($('#cgStatus').length) {
		$('#cgStatus').hide();
	}
}

function showError(message) {
	if ($('#cgStatus').length) {
		$('#cgStatus div:first-child').hide();
		$('#cgStatus div:nth-child(2)').html(message).show();
		$('#cgStatus').css('background', '#F33B2F').show();
		setTimeout(function () { $('#cgStatus').hide(); }, 3000);
	}
}

var initSwipe = function (slideSelector) {
	$(slideSelector).unbind("click");
	var pswpElement = document.querySelectorAll('.pswp')[0];
	var slides = [];
	$(slideSelector).each(function (i, el) {
		var slide = {};
		slide.src = $(this).attr('data-src');
		slide.w = $(this).attr('data-w');
		slide.h = $(this).attr('data-h');
		slide.title = $(this).attr('data-title');
		slides.push(slide);
		$(this).click(function() {
			var options = {
				index: i
			};
			var gallery = new PhotoSwipe(pswpElement, PhotoSwipeUI_Default, slides, options);
			gallery.init();
			return false;
		});
	});
}

