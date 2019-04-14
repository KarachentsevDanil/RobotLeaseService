<template>
    <div class="user-details">
        <div class="page-header">
            <div class="page-header-content">
                <div class="page-title">
                    <h4><i class="icon-users2 position-left"></i> <span class="text-semibold" v-localize="{i: 'rent.taxiDetails'}"></span></h4>

                    <ul class="breadcrumb position-right">
                        <li><a v-localize="{i: 'rent.rents'}"></a></li>
                        <li>
                            <router-link :to="'/rent-taxi'" v-localize="{i: 'rent.rentTaxi'}"></router-link>
                        </li>
                        <li class="active">{{taxi.AirTaxiCompanyName}} {{taxi.AirTaxiModelName}}</li>
                    </ul>
                </div>
                
                <div class="heading-elements">
                        <div class="heading-btn-group">
                            <a href="#" class="btn bg-blue btn-labeled heading-btn legitRipple" data-toggle="modal" data-target="#rentTaxi"><b><i class="icon-plus2"></i></b> <span v-localize="{i: 'rent.rentTaxi'}"></span></a>
                        </div>
                </div>
            </div>
        </div>
        <div class="content">
            <div class="panel panel-flat">
                <div class="tabbable">
                    <ul class="nav nav-tabs nav-tabs-bottom bottom-divided">
                        <li class="active"><a href="#taxi-details-tab" data-toggle="tab" v-localize="{i: 'common.details'}"></a></li>
                        <li><a href="#taxi-rents" data-toggle="tab" v-localize="{i: 'rent.rents'}"></a></li>
                    </ul>

                    <div class="tab-content">
                        <div class="tab-pane active" id="taxi-details-tab">
                            <div class="panel-body">
                                <div class="col-xs-12">
                                    <div class="form-horizontal">
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label" v-localize="{i: 'rent.owner'}"></label>
                                                <div class="col-sm-10">
                                                    <p class="form-control-static">{{taxi.CustomerName}}</p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label" v-localize="{i: 'rent.company'}"></label>
                                                <div class="col-sm-10">
                                                    <p class="form-control-static">{{taxi.AirTaxiCompanyName}}</p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label" v-localize="{i: 'rent.model'}"></label>
                                                <div class="col-sm-10">
                                                    <p class="form-control-static">{{taxi.AirTaxiModelName}}</p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label" v-localize="{i: 'rent.type'}"></label>
                                                <div class="col-sm-10">
                                                    <p class="form-control-static">{{taxi.AirTaxiTypeName}}</p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label" v-localize="{i: 'rent.description'}"></label>
                                                <div class="col-sm-10">
                                                    <p class="form-control-static">{{taxi.AirTaxiDescription}}</p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label" v-localize="{i: 'rent.capacity'}"></label>
                                                <div class="col-sm-10">
                                                    <p class="form-control-static">{{taxi.AirTaxiCapacity}}</p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label" v-localize="{i: 'rent.dailyCapacity'}"></label>
                                                <div class="col-sm-10">
                                                    <p class="form-control-static">{{taxi.DailyCosts}}</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="tab-pane" id="taxi-rents">
                            <taxi-rent-calendar :id="id"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <taxi-rent-popup :taxi="taxi"/>
    </div>
</template>

<script>
    import * as taxiService from "../../../air-taxi/pages/taxi/api/taxi-service";
    import taxiRentCalendar from './taxi-rent-calendar';
    import taxiRentPopup from './rent-taxi-popup';

    export default {
        props: {
            id: {
                required: true
            }
        },
        components:{
            taxiRentCalendar: taxiRentCalendar,
            taxiRentPopup: taxiRentPopup
        },
        data() {
            return {
                taxi: {}
            };
        },
        async beforeMount() {
            let taxi = (await taxiService.getAirTaxiById(this.id)).data.Data;
            this.taxi = taxi;
        }
    };
</script>

