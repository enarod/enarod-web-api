Moderation = {};

Moderation.post = function (path, params, arrayName, method) {
	method = method || "post";

	var form = document.createElement("form");
	form.setAttribute("method", method);
	form.setAttribute("action", path);

	if (params instanceof Array) {
		for (var i in params) {
			if (params.hasOwnProperty(i)) {
				for (var key in params[i]) {
					//if (params.hasOwnProperty(key)) {
					var propertyName = arrayName + "[" + i + "]." + key;
					var hiddenField = document.createElement("input");
					hiddenField.setAttribute("type", "hidden");
					hiddenField.setAttribute("id", propertyName);
					hiddenField.setAttribute("name", propertyName);
					hiddenField.setAttribute("value", params[i][key]);

					form.appendChild(hiddenField);
					//}
				}
			}
		}
	} else {
		for (var key in params) {
			if (params.hasOwnProperty(key)) {
				var hiddenField = document.createElement("input");
				hiddenField.setAttribute("type", "hidden");
				hiddenField.setAttribute("name", key);
				hiddenField.setAttribute("value", params[key]);

				form.appendChild(hiddenField);
			}
		}
	}

	document.body.appendChild(form);
	form.submit();
}