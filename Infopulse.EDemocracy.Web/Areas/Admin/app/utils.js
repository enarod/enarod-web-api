var Utils = (function() {
	var tokenKey = 'accessToken';

	function getAuthorizationHeader() {
		if (localStorage.getItem(tokenKey) === null) {
			return null;
		} else {
			return 'Bearer ' + localStorage.getItem(tokenKey);
		}
	};

	return {
		GetAuthorizationHeader: getAuthorizationHeader
	};
})();