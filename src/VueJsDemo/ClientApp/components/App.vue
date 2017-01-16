<template>
    <div class="panel panel-default">
        <div class="panel-heading">
            <nav>
                <ul class="list-inline">
                    <li v-if="!auth.user.authenticated">
                        <router-link :to="{ name: 'home' }">Home</router-link>
                    </li>
                    <li v-if="auth.user.authenticated">
                        <router-link :to="{ name: 'dashboard' }">Home</router-link>
                    </li>
                    <li class="pull-right" v-if="!auth.user.authenticated">
                        <router-link :to="{ name: 'register' }">Register</router-link>
                    </li>
                    <!--<li class="pull-right" v-if="!auth.user.authenticated">
                        <router-link :to="{ name: 'signin' }">Sign in</router-link>
                    </li>
                    <li class="pull-right" v-if="auth.user.authenticated">
                        <a href="javascript:void(0)" @click="signout">Sign out</a>
                    </li>-->
                    <li class="pull-right" v-if="!auth.user.authenticated">
                        <a href="javascript:void(0)" @click="signin">Sign in</a>
                    </li>
                    <li class="pull-right" v-if="auth.user.authenticated">
                        <a href="javascript:void(0)" @click="signout">Sign out</a>
                    </li>
                    <li class="pull-right" v-if="auth.user.authenticated">
                        Hi, {{ auth.user.profile.name }}
                    </li>
                </ul>
            </nav>
        </div>
        <div class="panel-body">
            <router-view></router-view>
        </div>
    </div>
</template>

<style>
    .container {
        padding-top: 40px;
    }
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