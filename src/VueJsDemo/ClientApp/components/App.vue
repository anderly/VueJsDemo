<template>
    <div>
    <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a asp-area="" asp-controller="Home" asp-action="Index" class="navbar-brand">VueJsDemo</a>
            </div>
            <div class="navbar-collapse collapse">
                <ul class="nav navbar-nav">
                    <li v-if="!auth.user.authenticated">
                        <router-link :to="{ name: 'home' }">Home</router-link>
                    </li>
                    <li v-if="auth.user.authenticated">
                        <router-link :to="{ name: 'dashboard' }">Home</router-link>
                    </li>
                    <!--<li class="pull-right" v-if="!auth.user.authenticated">
        <router-link :to="{ name: 'signin' }">Sign in</router-link>
    </li>
    <li class="pull-right" v-if="auth.user.authenticated">
        <a href="javascript:void(0)" @click="signout">Sign out</a>
    </li>-->
                </ul>
                <ul class="nav navbar-nav navbar-right">
                    <li v-if="!auth.user.authenticated">
                        <router-link :to="{ name: 'register' }">Register</router-link>
                    </li>
                    <li v-if="!auth.user.authenticated">
                        <a href="javascript:void(0)" @click="signin">Sign in</a>
                    </li>
                    <li class="dropdown">

                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" v-if="auth.user.authenticated">
                            <i class="icon-user"></i>
                            Hi, {{ auth.user.profile.name }}
                            <b class="caret"></b>
                        </a>

                        <ul class="dropdown-menu">
                            <li><a href="UserProfile" target="main" onclick="$('#breadcrumb').show();NextScreen('My Profile', 'UserProfile', true);top.resize();">My Profile</a></li>
                            <li>
                                <a href="My_Preferences.asp" target="main" onclick="$('#breadcrumb').show();NextScreen('My Preferences', 'My_Preferences.asp', true);top.resize();">My Preferences</a>
                                <!--<li class="divider"></li>-->
                            </li>
                            <li class="pull-right" v-if="auth.user.authenticated">
                                <a href="javascript:void(0)" @click="signout">Sign out</a>
                            </li>
                        </ul>

                    </li>
                    <!--<li class="pull-right" v-if="!auth.user.authenticated">
                        <a href="javascript:void(0)" @click="signin">Sign in</a>
                    </li>
                    <li class="pull-right" v-if="auth.user.authenticated">
                        <a href="javascript:void(0)" @click="signout">Sign out</a>
                    </li>
                    <li class="pull-right" v-if="auth.user.authenticated">
                        Hi, {{ auth.user.profile.name }}
                    </li>-->
                </ul>
            </div>
        </div>
    </div>
    <div class="container body-content">
        <router-view></router-view>
    </div>
        <hr />
        <footer>
            <p>&copy; 2017 - VueJsDemo</p>
        </footer>
    </div>
</template>

<style>
    /*.container {
        padding-top: 40px;
    }*/
</style>

<script>
import auth from '../auth.js'

export default {
    data() {
            return {
                auth: auth
            }
        },
        methods: {
            signin() {
                auth.signin()
            },
            signout() {
                auth.signout()
            }
        },
        created() {
            auth.check()
        }
}
</script>