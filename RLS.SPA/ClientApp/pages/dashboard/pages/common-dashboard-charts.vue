<template>
    <div>
        <div>
            <h6 class="content-group text-semibold">Detailed Statistics</h6>
            <div class="row">
                <div class="col-md-4">
                    <div class="panel text-center bg-purple-400 has-bg-image">
                        <div class="panel-body">
                            <h6 class="text-semibold no-margin-bottom mt-5">User statistics</h6>
                            <div class="opacity-75 content-group">{{statistics.UserCount}} users</div>
                            <div class="svg-center position-relative mb-5 padding-left-for-bar">
                                <radial-progress-bar :diameter="150"
                                                     :completed-steps="userProgressValue"
                                                     :total-steps="100"
                                                     :startColor="'#fff'"
                                                     :stopColor="'#fff'"
                                                     :innerStrokeColor="'purple'"
                                                     :strokeWidth="5">
                                    <p class="progress-statistic">{{ userProgressValue }}%</p>
                                </radial-progress-bar>
                            </div>
                        </div>

                        <div class="panel-body panel-body-accent pb-15">
                            <div class="row">
                                <div class="col-xs-4">
                                    <div class="text-uppercase text-size-mini opacity-75">With Robots</div>
                                    <h5 class="text-semibold no-margin">{{statistics.UserWithRobotsCount}}</h5>
                                </div>

                                <div class="col-xs-4">
                                    <div class="text-uppercase text-size-mini opacity-75">With Rents</div>
                                    <h5 class="text-semibold no-margin">{{statistics.UserWithRentsCount}}</h5>
                                </div>

                                <div class="col-xs-4">
                                    <div class="text-uppercase text-size-mini opacity-75">With Both</div>
                                    <h5 class="text-semibold no-margin">{{statistics.UserWithRobotsAndRentsCount}}</h5>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="panel text-center bg-danger-400 has-bg-image">
                        <div class="panel-body">
                            <h6 class="text-semibold no-margin-bottom mt-5">Available Robots for Today statistics</h6>
                            <div class="opacity-75 content-group">{{statistics.RobotCount}} all robots</div>
                            <div class="svg-center position-relative mb-5 padding-left-for-bar">
                                <radial-progress-bar :diameter="150"
                                                     :completed-steps="robotProgressValue"
                                                     :total-steps="100"
                                                     :startColor="'#fff'"
                                                     :stopColor="'#fff'"
                                                     :innerStrokeColor="'red'"
                                                     :strokeWidth="5">
                                    <p class="progress-statistic">{{ robotProgressValue }}%</p>
                                </radial-progress-bar>
                            </div>
                        </div>

                        <div class="panel-body panel-body-accent pb-15">
                            <div class="row">
                                <div class="col-xs-6">
                                    <div class="text-uppercase text-size-mini opacity-75">Available</div>
                                    <h5 class="text-semibold no-margin">{{statistics.AvailableTodayRobotCount}}</h5>
                                </div>

                                <div class="col-xs-6">
                                    <div class="text-uppercase text-size-mini opacity-75">Busy</div>
                                    <h5 class="text-semibold no-margin">
                                        {{statistics.RobotCount - statistics.AvailableTodayRobotCount}}
                                    </h5>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="panel text-center bg-success-400 has-bg-image">
                        <div class="panel-body">
                            <h6 class="text-semibold no-margin-bottom mt-5">Rents statistics</h6>
                            <div class="opacity-75 content-group">{{statistics.RentCount}} rents</div>
                            <div class="svg-center position-relative mb-5 padding-left-for-bar">
                                <radial-progress-bar :diameter="150"
                                                     :completed-steps="rentProgressValue"
                                                     :total-steps="100"
                                                     :startColor="'#fff'"
                                                     :stopColor="'#fff'"
                                                     :innerStrokeColor="'green'"
                                                     :strokeWidth="5">
                                    <p class="progress-statistic">{{ rentProgressValue }}%</p>
                                </radial-progress-bar>
                            </div>
                        </div>

                        <div class="panel-body panel-body-accent pb-15">
                            <div class="row">
                                <div class="col-xs-4">
                                    <div class="text-uppercase text-size-mini opacity-75">Completed</div>
                                    <h5 class="text-semibold no-margin">{{statistics.CompletedRentCount}}</h5>
                                </div>

                                <div class="col-xs-4">
                                    <div class="text-uppercase text-size-mini opacity-75">Active</div>
                                    <h5 class="text-semibold no-margin">{{statistics.ActiveRentCount}}</h5>
                                </div>

                                <div class="col-xs-4">
                                    <div class="text-uppercase text-size-mini opacity-75">Canceled</div>
                                    <h5 class="text-semibold no-margin">{{statistics.CanceledRentCount}}</h5>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <h6 class="content-group text-semibold">Widget Statistics</h6>
            <div class="row">
                <div class="col-sm-6 col-md-3">
                    <div class="panel panel-body bg-purple-400 has-bg-image">
                        <div class="media no-margin">
                            <div class="media-body">
                                <h3 class="no-margin">{{statistics.RobotCount}}</h3>
                                <span class="text-uppercase text-size-mini">total robots</span>
                            </div>

                            <div class="media-right media-middle">
                                <i class="icon-android icon-3x opacity-75"></i>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-sm-6 col-md-3">
                    <div class="panel panel-body bg-danger-400 has-bg-image">
                        <div class="media no-margin">
                            <div class="media-body">
                                <h3 class="no-margin">{{statistics.CompanyCount}}</h3>
                                <span class="text-uppercase text-size-mini">total companies</span>
                            </div>

                            <div class="media-right media-middle">
                                <i class="icon-bag icon-3x opacity-75"></i>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6 col-md-3">
                    <div class="panel panel-body bg-blue-400 has-bg-image">
                        <div class="media no-margin">
                            <div class="media-body">
                                <h3 class="no-margin">{{statistics.ModelCount}}</h3>
                                <span class="text-uppercase text-size-mini">total robot models</span>
                            </div>

                            <div class="media-right media-middle">
                                <i class="icon-android icon-3x opacity-75"></i>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6 col-md-3">
                    <div class="panel panel-body bg-success-400 has-bg-image">
                        <div class="media no-margin">
                            <div class="media-body">
                                <h3 class="no-margin">{{statistics.TypeCount}}</h3>
                                <span class="text-uppercase text-size-mini">total robot types</span>
                            </div>

                            <div class="media-right media-middle">
                                <i class="icon-bag icon-3x opacity-75"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
    import * as robotService from "../../air-robot/pages/robot/api/robot-service";

    export default {
        data() {
            return {
                userProgressValue: 0,
                robotProgressValue: 0,
                rentProgressValue: 0,
                statistics: {}
            };
        },
        async beforeMount() {
            this.statistics = (await robotService.getStatistics()).data.Data;

            this.userProgressValue = Math.round(
                (this.statistics.UserWithRentsCount / this.statistics.UserCount) * 100
            );
            this.robotProgressValue = Math.round(
                (this.statistics.AvailableTodayRobotCount / this.statistics.RobotCount) *
                100
            );
            this.rentProgressValue = Math.round(
                ((this.statistics.CompletedRentCount + this.statistics.ActiveRentCount) /
                    this.statistics.RentCount) *
                100
            );
        },
        methods: {
            fillChartData(chartSettings, data, chartType) {
                chartSettings.data = data;

                chartSettings.title.name = this.$locale({
                    i: "chart." + chartType + ".pieChartName"
                });

                chartSettings.title.text = this.$locale({
                    i: "chart." + chartType + ".pieChartTitle"
                });

                chartSettings.title.subtext = this.$locale({
                    i: "chart." + chartType + ".rentPieChartSubText"
                });
            }
        }
    };
</script>

<style>
    .padding-left-for-bar {
        padding-left: 29%;
    }

    p.progress-statistic {
        font-size: 22px;
        line-height: 1;
        color: white;
        text-anchor: middle;
        margin-left: 5px;
        margin-top: 5px;
    }
</style>
