import RentListPage from "../pages/rent/pages/rent-list/rent-list";
import OwnerRentListPage from "../pages/rent/pages/rent-list/owner-rent-list";
import RentTaxiPage from "../pages/rent/pages/rent-taxi/rent-taxi";
import TaxiDetailsPage from '../pages/rent/pages/rent-taxi/taxi-rent-details';
import RentDetailsPage from '../pages/rent/pages/rent-details/rent-details';

import * as routeGuards from "./route-guards";

export default [
    {
        path: "/rent-list",
        component: RentListPage,
        beforeEnter: (to, from, next) => {
            routeGuards.validateRoute(to, from, next);
        }
    },
    {
        path: "/owner-rent-list",
        component: OwnerRentListPage,
        beforeEnter: (to, from, next) => {
            routeGuards.validateRoute(to, from, next);
        }
    },
    {
        path: "/rent-taxi",
        component: RentTaxiPage,
        beforeEnter: (to, from, next) => {
            routeGuards.validateRoute(to, from, next);
        }
    },
    {
        path: "/taxi-details/:id",
        props: true,
        component: TaxiDetailsPage,
        beforeEnter: (to, from, next) => {
            routeGuards.validateRoute(to, from, next);
        }
    },
    {
        path: "/rent-details/:id",
        props: true,
        component: RentDetailsPage,
        beforeEnter: (to, from, next) => {
            routeGuards.validateRoute(to, from, next);
        }
    }
];