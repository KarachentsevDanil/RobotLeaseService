import Vue from "vue";
import VueRouter from "vue-router";
import HomePage from './pages/layout/home-page'
import LoginPage from "./pages/auth/pages/login";

import authorizationRoutes from "./routes/authorization-routes";
import airTaxiRoutes from "./routes/air-taxi-routes";
import rentRoutes from "./routes/rent-routes";

Vue.use(VueRouter);

const routes = [
    ...authorizationRoutes,
    ...airTaxiRoutes,
    ...rentRoutes,
    {
        path: "*",
        component: LoginPage
    }
];

let router = new VueRouter({
    mode: "history",
    routes
});

export default router;
