function AdminPetitionsViewModel() {
	var self = this;

	self.init = function() {
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

	self.petitions = ko.observableArray(petitions); // window.petitions
	self.selectedPetitions = ko.observableArray();

	self.getPetitions = function() {
		if (!localStorage["accessToken"]) {
			self.showLoginDialog();
		} else {
			$.ajax({
				url: "/api/admin/petitions",
				type: "GET",
				//data: JSON.stringify(data),
				beforeSend: function (xhr) {
					var token = localStorage["accessToken"];
					xhr.setRequestHeader("Authorization", "Basic " + token);
				}
			})
			.done(function (responseData) {
				self.petitions = responseData.Data;
			})
			.error(function (jqXHR, textStatus, errorThrown) {
				console.log("Error: " + errorThrown);

				// if error code is 401 Unauthorized then show login window
				if (errorThrown === "Unauthorized") {
					self.showLoginDialog();
				}
			});
		}
	};

	self.showLoginDialog = function() {
		console.log("not logged in. showing login dialog");

		$("#loginForm").dialog("open");
	};

	self.signIn = function() {

	};

	self.assignToMe = function () {
		console.log("assignToMe");
		var data = [];
		var selectedPetitions = self.selectedPetitions();
		for (var i in selectedPetitions) {
			if (selectedPetitions.hasOwnProperty(i)) {
				data.push({ ID: selectedPetitions[i].ID });
			}
		}
		sendPostRequest("/api/admin/petitions/AssignToMe", data);
	};
	self.approveSelectedPetitions = function () {
		console.log("approveSelectedPetitions");
	};
	self.rejectSelectedPetitions = function () {
		console.log("rejectSelectedPetitions");
	};
	self.escalateSelectedPetitions = function () {
		console.log("escalateSelectedPetitions");
	};

	function sendPostRequest(url, data) {
		$.ajax({
			type: "POST",
			url: url,
			data: JSON.stringify(data)
		})
		.done(function (responseData) {
			//window.location = window.location; // refresh page
		})
		.error(function (jqXHR, textStatus, errorThrown) {
			console.log("Error: " + errorThrown);
		});
	}
}

function LoginFormViewModel() {
	var self = this;

	self.login = ko.observable("");
	self.password = ko.observable("");

	self.signIn = function() {
		$.ajax({
			url: "/api/account/signin",
			type: "POST",
			body: {
				grant_type: "password",
				username: self.login,
				password: self.password
			},
			contentType: "application/x-www-form-urlencoded",
			beforeSend: function (xhr) {
				xhr.setRequestHeader("Accept", "application\json");
			}
		})
		.done(function(data) {
			localStorage.setItem("accessToken", data.access_token);
		})
		.error(function(xhr, status, error) {
			debugger;
		});
	};

	self.signOut = function() {
		localStorage.setItem("accessToken", null);
	};
}