function AuthViewModel() {
	var self = this;

	var tokenKey = 'accessToken';

	self.init = function () {
		updateAuthHeader();

		ko.postbox.subscribe('signInRequested', function (e) {
			self.showLoginDialog();
		});

		$("#loginForm").dialog({
			autoOpen: false,
			height: 300,
			width: 350,
			modal: true,
			buttons: {
				"Sign in": self.signIn,
				Cancel: function () {
					console.log("singIn form close button clicked");
					$("#loginForm").dialog("close");
				}
			},
			close: function () {
				console.log("singIn form closed");
			}
		});
	};

	self.login = ko.observable("");
	self.password = ko.observable("");
	self.errorText = ko.observable("");
	self.authorizeHeader = ko.observable("");

	self.showLoginDialog = function () {
		console.log("not logged in. showing login dialog");

		$("#loginForm").dialog("open");
	};

	self.signIn = function () {
		$.ajax({
			url: "/api/account/signin",
			type: "POST",
			data: {
				grant_type: "password",
				username: self.login(),
				password: self.password()
			},
			accepts: 'application/json',
			contentType: 'application/x-www-form-urlencoded',
			beforeSend: function (xhr) {
			}
		})
		.done(function (data) {
			localStorage.setItem(tokenKey, data.access_token);
			self.errorText("");
			updateAuthHeader();

			$("#loginForm").dialog("close");
		})
		.error(function (xhr, status, error) {
			if (xhr.responseText && xhr.responseText.length > 0) {
				var response = JSON.parse(xhr.responseText);
				self.errorText(response.error_description);
			}
		});
	};

	self.signOut = function () {
		localStorage.clear(tokenKey);
		updateAuthHeader();
	};

	self.isAuthorized = ko.computed(function () {
		return self.authorizeHeader() != null;
	});

	var updateAuthHeader = function() {
		self.authorizeHeader(Utils.GetAuthorizationHeader());
	};
}