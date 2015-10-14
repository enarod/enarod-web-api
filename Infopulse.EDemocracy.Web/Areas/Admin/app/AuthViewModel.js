function AuthViewModel() {
	var self = this;

	var tokenKey = 'accessToken';

	self.init = function () {
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

	ko.postbox.subscribe('signInRequested', function() {
		self.showLoginDialog();
	});

	self.login = ko.observable("");
	self.password = ko.observable("");

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
				////xhr.setRequestHeader("Accept", "application\json");
			}
		})
		.done(function (data) {
			localStorage.setItem(tokenKey, data.access_token);
		})
		.error(function (xhr, status, error) {
			debugger;
		});
	};

	self.signOut = function () {
		localStorage.setItem(tokenKey, null);
	};

	self.showLoginDialog = function () {
		console.log("not logged in. showing login dialog");

		$("#loginForm").dialog("open");
	};

	self.isAuthorized = ko.computed(function () {
		return localStorage.getItem(tokenKey) != null;
	});
}