function PetitionsViewModel() {
	var self = this;

	self.init = function() {
		
	};

	self.petitions = ko.observableArray();
	self.selectedPetitions = ko.observableArray();

	self.getPetitions = function () {
		var authorizationHeader = Utils.GetAuthorizationHeader();

		if (authorizationHeader != null) {
			$.ajax({
				url: "/api/admin/petitions",
				type: "GET",
				contentType: "application/json",
				beforeSend: function (xhr) {
					xhr.setRequestHeader("Authorization", authorizationHeader);
				}
			})
			.done(function (responseData) {
				self.petitions(responseData.Data);
			})
			.error(function (jqXHR, textStatus, errorThrown) {
				console.log("Error: " + errorThrown);
				alert('Incorrect access token. Please re-signin.');
				// if error code is 401 Unauthorized then show login window
				if (errorThrown === "Unauthorized") {
					ko.postbox.publish('signInRequested');
				}
			});
		} else {
			ko.postbox.publish('signInRequested');
		}
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