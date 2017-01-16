var Vue = require('vue');
var VueRouter = require('vue-router');
var VueResource = require('vue-resource');

import App from './components/App.vue';
import Dashboard from './components/Dashboard.vue';
import Home from './components/Home.vue';
import Register from './components/Register.vue';
import Signin from './components/Signin.vue';
import auth from './auth';

Vue.use(VueRouter);
Vue.use(VueResource);

Vue.config.devtools = true;

//Vue.http.headers.common['X-CSRF-TOKEN'] = document.getElementsByName('csrf-token')[0].getAttribute('content');
//Vue.http.headers.common['Authorization'] = 'Bearer ' + localStorage.getItem('adal.idtoken');
Vue.http.options.root = 'http://localhost:5000';

export default Vue;

export var router = new VueRouter({
    //mode: 'history',
    root: '/',
    routes: [
        {
            path: '/',
            name: 'home',
            component: Home
        },
        {
            path: '/dashboard',
            name: 'dashboard',
            component: Dashboard,
            auth: true
        },
        {
            path: '/register',
            name: 'register',
            component: Register
        },
        {
            path: '/signin',
            name: 'signin',
            component: Signin
        }
    ]
});

// Auth middleware
//router.beforeEach(function(transition) {
//    if (transition.to.auth && !auth.user.authenticated) {
//        // if route requires auth and user isn't authenticated
//        transition.redirect('/signin');
//    } else {
//        transition.next();
//    }
//});

//router.start(Vue.extend({
//    data: function() {
//        return { user: {} };
//    },
//    computed: {
//        auth: function() {
//            return auth;
//        }
//    },
//    methods: {
//        checkLocalStorage: function() {
//            auth.check();
//        },
//        logout: function() {
//            auth.logOut();
//        }
//    },
//    ready: function() {
//        this.checkLocalStorage();
//    }
//}), '#app');

Vue.http.interceptors.push(function (request, next) {
    //Add JWT to all requests
    request.headers.set('Authorization', 'Bearer ' + localStorage.getItem('adal.idtoken'));
    next();
});

window.App = new Vue({
    el: '#app',
    router: router,
    render: app => app(App)
});