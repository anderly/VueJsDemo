<template>
    <div>
        <h1>Dashboard</h1>

        <h4>Contact List:</h4>
        <div class="alert alert-danger" v-if="error">
            <p>
                <strong>Error(s) occurred:</strong>
                <li v-for="error in errors">{{ error.message }}</li>
            </p>
        </div>
        <form autocomplete="off" v-on:submit="addContact">
            <div class="form-group">
                <label for="firstName">First Name</label>
                <input type="firstName" id="firstName" class="form-control" v-model="form.firstName" required>
            </div>
            <div class="form-group">
                <label for="lastName">Last Name</label>
                <input type="lastName" id="lastName" class="form-control" v-model="form.lastName" required>
            </div>
            <div class="form-group">
                <label for="email">Email</label>
                <input type="email" id="email" class="form-control" placeholder="user@company.com" v-model="form.email" required>
            </div>
            <div class="form-group">
                <label for="mobilePhone">Phone</label>
                <input type="text" id="mobilePhone" class="form-control" v-model="form.mobilePhone" required>
            </div>
            <button type="submit" class="btn btn-default">Add</button>
        </form>
        <table class="table table-striped">
            <thead>
                <tr>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Email</th>
                    <th>Phone</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="contact in contacts">
                    <td>{{ contact.firstName }}</td>
                    <td>{{ contact.lastName }}</td>
                    <td>{{ contact.email }}</td>
                    <td>{{ contact.mobilePhone }}</td>
                    <td><a href="javascript:void(0)" @click="deleteContact(contact)">Delete</a></td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
    import auth from '../auth';

    export default {
        data() {
            return {
                form: {
                    firstName: auth.user.profile.name.split(' ')[0],
                    lastName: auth.user.profile.name.split(' ')[1],
                    email: auth.user.profile.upn,
                    mobilePhone: ''

                },
                success: false,
                error: false,
                errors: [],
                contacts: []
            }
        },
    methods: {
        addContact(event) {
            var self = this;
            event.preventDefault()
            // HTTP POST /api/contacts
            this.$http.post('api/contacts', this.form).then(response => {
                self.reset();
                self.getContacts();
            }, response => {
                console.log(response);
                self.error = true;
                self.errors = response.body.errors;
            });
        },
        deleteContact(contact) {
            var self = this;
            this.resetErrors();
            if (auth.user.profile.upn !== contact.email) {
                self.error = true;
                self.errors = [
                    { message: "You can only delete contacts matching your email address (" + auth.user.profile.upn + ')' }
                ];
                return;
            }
            this.$http.delete('api/contacts/' + contact.mobilePhone, this.form).then(response => {
                self.reset();
                self.getContacts();
            }, response => {
                console.log(response);
                self.error = true;
                self.errors = response.body.errors;
            });
        },
        getContacts() {
            this.$http.get('api/contacts').then(response => {
                this.contacts = response.body;
            });
        },
        reset() {
            this.resetErrors();
            this.resetForm();
           
        },
        resetErrors() {
            this.error = false;
            this.errors = [];
        },
        resetForm() {
            this.form = { firstName: '', lastName: '', email: '', mobilePhone: '' };
        }
    },
        created() {
            // HTTP GET /api/contacts
            this.getContacts();
        }
    }
</script>