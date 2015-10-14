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
				// if error code is 401 Unauthorized then show login window
				if (errorThrown === "Unauthorized") {
					alert('Incorrect access token. Please re-signin.');
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

		$.ajax({
			url: "/api/admin/petitions/AssignToMe",
			type: "POST",
			data: JSON.stringify(data),
			contentType: "application/json",
			beforeSend: function (xhr) {
				xhr.setRequestHeader("Authorization", Utils.GetAuthorizationHeader());
			}
		})
		.done(function (responseData) {
			
		})
		.error(function (jqXHR, textStatus, errorThrown) {
			console.log("Error: " + errorThrown);
			
			// if error code is 401 Unauthorized then show login window
			switch (errorThrown) {
				case "Unauthorized":
				{
					alert('Incorrect access token. Please re-signin.');
					ko.postbox.publish('signInRequested');
					break;
				}
				case "Internal Server Error":
				{
					alert(jqXHR.responseJSON.ExceptionMessage);
					break;
				}
				default:
				{
					console.log(jqXHR);
					break;
				}
			}
		});
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