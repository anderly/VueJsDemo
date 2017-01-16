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
                </tr>
            </thead>
            <tbody>
                <tr v-for="contact in contacts">
                    <td>{{ contact.firstName }}</td>
                    <td>{{ contact.lastName }}</td>
                    <td>{{ contact.email }}</td>
                    <td>{{ contact.mobilePhone }}</td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                form: {
                    firstName: '',
                    lastName: '',
                    email: '',
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
                self.getContacts();
                self.form = { firstName: '', lastName: '', email: '', mobilePhone: '' };
            }, response => {
                console.log(response);
                self.error = true;
                self.errors.push(response.body);
            });
        },
        getContacts() {
            this.$http.get('api/contacts').then(response => {
                this.contacts = response.body;
            });
        }
    },
        created() {
            // HTTP GET /api/contacts
            this.getContacts();
        }
    }
</script>