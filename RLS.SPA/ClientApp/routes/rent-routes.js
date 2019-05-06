import RentListPage from "../pages/rent/pages/rent-list/rent-list";
import OwnerRentListPage from "../pages/rent/pages/rent-list/owner-rent-list";
import RentrobotPage from "../pages/rent/pages/rent-robot/rent-robot";
import robotDetailsPage from '../pages/rent/pages/rent-robot/robot-rent-details';
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
        path: "/rent-robot",
        component: RentrobotPage,
        beforeEnter: (to, from, next) => {
            routeGuards.validateRoute(to, from, next);
        }
    },
    {
        path: "/robot-details/:id",
        props: true,
        component: robotDetailsPage,
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
