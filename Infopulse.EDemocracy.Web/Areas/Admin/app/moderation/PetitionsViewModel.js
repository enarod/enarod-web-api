function PetitionsViewModel() {
	var self = this;

	self.init = function() {
		ko.postbox.subscribe('signedIn', function(e) {
			self.getPetitions();
		});
	};

	self.petitions = ko.observableArray();
	self.selectedPetitions = ko.observableArray();

	self.buttonIsEnabled = ko.computed(function () {
		return self.selectedPetitions.length > 0;
	});

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
				for (var petitionIndex in responseData.Data) {
					if (responseData.Data.hasOwnProperty(petitionIndex)) {
						var petition = responseData.Data[petitionIndex];
						petition.ApproverEmail = (petition.Approver && petition.Approver.User)
							? petition.Approver.User.Email
							: '';
						petition.ApprovedDate = petition.ApprovedDate == null
							? ''
							: moment(petition.ApprovedDate).format('DD.MM.YYYY hh:mm');
						if (petition.PetitionStatus.ID === 1 && petition.Approver != null) {
							petition.AdminStatus = 'На модерації';
						} else if (petition.PetitionStatus.ID === 1 && petition.Approver == null) {
							petition.AdminStatus = 'Створена';
						} else {
							petition.AdminStatus = petition.PetitionStatus.Name;
						}
					}
				}
				self.petitions(responseData.Data);

				var grid = new Slick.Grid("#petitionsGrid", self.petitions(), columns, options);
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

	function getSelectedPetitions() {
		var data = [];
		var selectedPetitions = self.selectedPetitions();
		for (var i = 0; i < selectedPetitions.length; i++) {
			data.push({ ID: selectedPetitions[i].ID });
		}

		return data;
	}

	self.assignToMe = function () {
		console.log("assignToMe");
		//var data = [];
		//var selectedPetitions = self.selectedPetitions();
		//for (var i in selectedPetitions) {
		//	if (selectedPetitions.hasOwnProperty(i)) {
		//		data.push({ ID: selectedPetitions[i].ID });
		//	}
		//}

		var data = getSelectedPetitions();

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
			window.location.href = '/admin/petitions/';
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
		var data = getSelectedPetitions();

		$.ajax({
			url: "/api/admin/petitions/approve",
			type: "POST",
			data: JSON.stringify(data),
			contentType: "application/json",
			beforeSend: function (xhr) {
				xhr.setRequestHeader("Authorization", Utils.GetAuthorizationHeader());
			}
		})
		.done(function (responseData) {
			window.location.href = '/admin/petitions/';
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