<template>
    <div>
        <div class="panel panel-body login-form">
            <div class="text-center">
                <div class="icon-object border-slate-300 text-slate-300"><i class="icon-reading"></i>
                </div>
                <h5 class="content-group">
                    <span v-localize="{i: 'authorization.loginToAccount'}"></span>
                    <small v-localize="{i: 'authorization.credentials'}" class="display-block"></small></h5>
            </div>

            <div class="form-group has-feedback has-feedback-left">
                <input type="text" v-localize="{i: 'authorization.email', attr: 'placeholder'}" class="form-control"
                                          v-model="user.email"
                                          :error-messages="emailErrors"
                                          @input="$v.user.email.$touch()"
                                          @blur="$v.user.email.$touch()"
                                          required >

                <div class="form-control-feedback">
                    <i class="icon-user text-muted"></i>
                </div>
            </div>

            <div class="form-group has-feedback has-feedback-left">
                <input type="password" class="form-control" v-model="user.password"
                                          :error-messages="passwordErrors"
                                          v-localize="{i: 'authorization.password', attr: 'placeholder'}"
                                          @input="$v.user.password.$touch()"
                                          @blur="$v.user.password.$touch()">

                <div class="form-control-feedback">
                    <i class="icon-lock2 text-muted"></i>
                </div>
            </div>

            <div class="form-group">
                <button type="submit" @click="submit" :disabled="isInvaild" class="btn bg-blue btn-block">
                 <span v-localize="{i: 'authorization.login'}"></span>
                 <i class="icon-arrow-right14 position-right"></i>
                </button>
            </div>
            
            <div class="content-divider text-muted form-group"><span v-localize="{i: 'authorization.createNewMessage'}"></span></div>
            <router-link to="/registr" class="btn btn-default btn-block content-group" v-localize="{i: 'authorization.signUp'}">
            </router-link>
        </div>
    </div>
</template>

<script>
    import * as authActions from "../store/types/action-types";
    import * as authGetters from "../store/types/getter-types";
    import * as authResources from "../store/resources";
    import * as authTextResources from "../resources/resources";
    import * as routeGuards from '../../../routes/route-guards';

    import { mapGetters } from "vuex";
    import { validationMixin } from "vuelidate";
    import { required, minLength, email } from "vuelidate/lib/validators";

    export default {
        mixins: [validationMixin],

        validations: {
            user: {
                email: { required, email },
                password: { required, minLength: minLength(6) }
            }
        },
        data: () => ({
            user: {
                email: "",
                password: "",
                rememberMe: false
            },
            labels: {
                ...authTextResources.lables
            }
        }),
        methods: {
            submit() {
                let data = {
                    user: {
                        Email: this.user.email,
                        Password: this.user.password,
                        RememberMe: this.user.rememberMe
                    },
                    router: this.$router,
                    notification: this.$noty
                };

                this.$store.dispatch(
                    authResources.AUTH_STORE_NAMESPACE.concat(authActions.LOGIN_ACTION),
                    data
                );
            }
        },
        computed: {
            isInvaild() {
                return this.$v.$invalid;
            },
            ...mapGetters({
                getUsername: authResources.AUTH_STORE_NAMESPACE.concat(
                    authGetters.GET_USER_GETTER
                ),
                getToken: authResources.AUTH_STORE_NAMESPACE.concat(
                    authGetters.GET_TOKEN_GETTER
                )
            }),
            passwordErrors() {
                const errors = [];
                if (!this.$v.user.password.$dirty) return errors;
                !this.$v.user.password.minLength &&
                    errors.push(authTextResources.lables.validationMessages.passwordLengthMessage);
                !this.$v.user.password.required && errors.push(authTextResources.lables.validationMessages.passwordRequiredMessage);
                return errors;
            },
            emailErrors() {
                const errors = [];
                if (!this.$v.user.email.$dirty) return errors;
                !this.$v.user.email.email && errors.push(authTextResources.lables.validationMessages.emailMessage);
                !this.$v.user.email.required && errors.push(authTextResources.lables.validationMessages.emailRequiredMessage);
                return errors;
            }
        },
        beforeRouteEnter: (to, from, next) => {
            routeGuards.redirectToHomePage(to, from, next);
        }
    };
</script>
