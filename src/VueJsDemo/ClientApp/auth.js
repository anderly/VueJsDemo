require('./adal.js');

// Enter Global Config Values & Instantiate ADAL AuthenticationContext
var authConfig = {
    instance: 'https://login.microsoftonline.com/',
    tenant: 'common', //'1a2cd35e-5a38-49db-a88f-ba3c4c8b52fd', //COMMON OR YOUR TENANT ID

    clientId: 'd77422a5-4646-4379-8f66-3dd471e586c7', //REPLACE WITH YOUR CLIENT ID
    redirectUri: 'http://localhost:5000/', //REPLACE WITH YOUR REDIRECT URL
    postLogoutRedirectUri: window.location.origin,
    cacheLocation: 'localStorage', // enable this for IE, as sessionStorage does not work for localhost.

    //callback: userSignedIn,
    popUp: true
};

window.ADAL = new AuthenticationContext(authConfig);

export default {
    user: {
            authenticated: false,
            profile: null
    },
    check() {
        if (localStorage.getItem('adal.idtoken') !== null) {
            var self = this;
            //Vue.http.get(
            //    'api/user',
            //).then(response => {
            //    this.user.authenticated = true
            //    this.user.profile = response.data.data
            //})
            var user = ADAL.getCachedUser();
            console.log(user);
            if (user != null) {
                self.user.authenticated = true;
                self.user.profile = user.profile;
            }
        }
    },
    register(context, name, email, password) {
        Vue.http.post(
            'api/register',
            {
                name: name,
                email: email,
                password: password
            }
        ).then(response => {
            context.success = true
        }, response => {
            context.response = response.data
            context.error = true
        })
    },
    signin(context, email, password) {
        var self = this;
        ADAL.callback = function(err, token) {
            //ADAL.oauth2Callback();
            console.log('userSignedIn called');
            if (!err) {
                console.log("token: " + token);
                //showWelcomeMessage();
                self.user.authenticated = true;
                var user = ADAL.getCachedUser();
                console.log(user);
                self.user.profile = user.profile;
                App.$router.push({
                    name: 'dashboard'
                });
            }
            else {
                console.error("error: " + err);
            }
        }
        ADAL.login();
        //Vue.http.post(
        //    'api/signin',
        //    {
        //        email: email,
        //        password: password
        //    }
        //).then(response => {
        //    context.error = false
        //    localStorage.setItem('id_token', response.data.meta.token)
        //    Vue.http.headers.common['Authorization'] = 'Bearer ' + localStorage.getItem('id_token')

        //    this.user.authenticated = true
        //    this.user.profile = response.data.data

        //    router.push({
        //        name: 'dashboard'
        //    })
        //}, response => {
        //    context.error = true
        //})
    },
    signout() {
        //localStorage.removeItem('adal.idtoken');
        ADAL.logOut();
        this.user.authenticated = false;
        this.user.profile = null;

        router.push({
            name: 'home'
        })
    }
}