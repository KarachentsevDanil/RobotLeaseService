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
              <div class="svg-center position-relative mb-5" id="progress_percentage_three">
                <svg width="92" height="92">
                  <g transform="translate(46,46)">
                    <path
                      class="d3-progress-background"
                      d="M0,46A46,46 0 1,1 0,-46A46,46 0 1,1 0,46M0,43A43,43 0 1,0 0,-43A43,43 0 1,0 0,43Z"
                      style="fill: rgb(3, 155, 229);"
                    ></path>
                    <path
                      class="d3-progress-foreground"
                      filter="url(#blur)"
                      d="M3.3306690738754696e-15,-44.47471191587417A1.5,1.5 0 0,1 1.5505617977528126,-45.97385950854408A46,46 0 1,1 -42.174613668366625,18.365782366238133A1.5,1.5 0 0,1 -41.351541356033856,16.372233429811033L-41.351541356033856,16.372233429811033A1.5,1.5 0 0,1 -39.424095385647064,17.16801395104869A43,43 0 1,0 1.449438202247194,-42.97556432320425A1.5,1.5 0 0,1 3.3306690738754696e-15,-44.47471191587417Z"
                      style="fill: rgb(255, 255, 255); stroke: rgb(255, 255, 255);"
                    ></path>
                    <path
                      class="d3-progress-front"
                      d="M3.3306690738754696e-15,-44.47471191587417A1.5,1.5 0 0,1 1.5505617977528126,-45.97385950854408A46,46 0 1,1 -42.174613668366625,18.365782366238133A1.5,1.5 0 0,1 -41.351541356033856,16.372233429811033L-41.351541356033856,16.372233429811033A1.5,1.5 0 0,1 -39.424095385647064,17.16801395104869A43,43 0 1,0 1.449438202247194,-42.97556432320425A1.5,1.5 0 0,1 3.3306690738754696e-15,-44.47471191587417Z"
                      style="fill: rgb(255, 255, 255); fill-opacity: 1;"
                    ></path>
                    <text
                      dx="0"
                      dy="8"
                      style="font-size: 22px; line-height: 1; fill: rgb(255, 255, 255); text-anchor: middle;"
                    >69%</text>
                  </g>
                </svg>
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
              <div class="svg-center position-relative mb-5" id="progress_percentage_three">
                <svg width="92" height="92">
                  <g transform="translate(46,46)">
                    <path
                      class="d3-progress-background"
                      d="M0,46A46,46 0 1,1 0,-46A46,46 0 1,1 0,46M0,43A43,43 0 1,0 0,-43A43,43 0 1,0 0,43Z"
                      style="fill: rgb(3, 155, 229);"
                    ></path>
                    <path
                      class="d3-progress-foreground"
                      filter="url(#blur)"
                      d="M3.3306690738754696e-15,-44.47471191587417A1.5,1.5 0 0,1 1.5505617977528126,-45.97385950854408A46,46 0 1,1 -42.174613668366625,18.365782366238133A1.5,1.5 0 0,1 -41.351541356033856,16.372233429811033L-41.351541356033856,16.372233429811033A1.5,1.5 0 0,1 -39.424095385647064,17.16801395104869A43,43 0 1,0 1.449438202247194,-42.97556432320425A1.5,1.5 0 0,1 3.3306690738754696e-15,-44.47471191587417Z"
                      style="fill: rgb(255, 255, 255); stroke: rgb(255, 255, 255);"
                    ></path>
                    <path
                      class="d3-progress-front"
                      d="M3.3306690738754696e-15,-44.47471191587417A1.5,1.5 0 0,1 1.5505617977528126,-45.97385950854408A46,46 0 1,1 -42.174613668366625,18.365782366238133A1.5,1.5 0 0,1 -41.351541356033856,16.372233429811033L-41.351541356033856,16.372233429811033A1.5,1.5 0 0,1 -39.424095385647064,17.16801395104869A43,43 0 1,0 1.449438202247194,-42.97556432320425A1.5,1.5 0 0,1 3.3306690738754696e-15,-44.47471191587417Z"
                      style="fill: rgb(255, 255, 255); fill-opacity: 1;"
                    ></path>
                    <text
                      dx="0"
                      dy="8"
                      style="font-size: 22px; line-height: 1; fill: rgb(255, 255, 255); text-anchor: middle;"
                    >69%</text>
                  </g>
                </svg>
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
                  <h5
                    class="text-semibold no-margin"
                  >{{statistics.RobotCount - statistics.AvailableTodayRobotCount}}</h5>
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
              <div class="svg-center position-relative mb-5" id="progress_percentage_three">
                <svg width="92" height="92">
                  <g transform="translate(46,46)">
                    <path
                      class="d3-progress-background"
                      d="M0,46A46,46 0 1,1 0,-46A46,46 0 1,1 0,46M0,43A43,43 0 1,0 0,-43A43,43 0 1,0 0,43Z"
                      style="fill: rgb(3, 155, 229);"
                    ></path>
                    <path
                      class="d3-progress-foreground"
                      filter="url(#blur)"
                      d="M3.3306690738754696e-15,-44.47471191587417A1.5,1.5 0 0,1 1.5505617977528126,-45.97385950854408A46,46 0 1,1 -42.174613668366625,18.365782366238133A1.5,1.5 0 0,1 -41.351541356033856,16.372233429811033L-41.351541356033856,16.372233429811033A1.5,1.5 0 0,1 -39.424095385647064,17.16801395104869A43,43 0 1,0 1.449438202247194,-42.97556432320425A1.5,1.5 0 0,1 3.3306690738754696e-15,-44.47471191587417Z"
                      style="fill: rgb(255, 255, 255); stroke: rgb(255, 255, 255);"
                    ></path>
                    <path
                      class="d3-progress-front"
                      d="M3.3306690738754696e-15,-44.47471191587417A1.5,1.5 0 0,1 1.5505617977528126,-45.97385950854408A46,46 0 1,1 -42.174613668366625,18.365782366238133A1.5,1.5 0 0,1 -41.351541356033856,16.372233429811033L-41.351541356033856,16.372233429811033A1.5,1.5 0 0,1 -39.424095385647064,17.16801395104869A43,43 0 1,0 1.449438202247194,-42.97556432320425A1.5,1.5 0 0,1 3.3306690738754696e-15,-44.47471191587417Z"
                      style="fill: rgb(255, 255, 255); fill-opacity: 1;"
                    ></path>
                    <text
                      dx="0"
                      dy="8"
                      style="font-size: 22px; line-height: 1; fill: rgb(255, 255, 255); text-anchor: middle;"
                    >69%</text>
                  </g>
                </svg>
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
                <h3 class="no-margin">{{statistics.RentCount}}</h3>
                <span class="text-uppercase text-size-mini">total rents</span>
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
                <h3 class="no-margin">{{statistics.CompletedRentCount}}</h3>
                <span class="text-uppercase text-size-mini">total completed rents</span>
              </div>

              <div class="media-right media-middle">
                <i class="icon-bag icon-3x opacity-75"></i>
              </div>
            </div>
          </div>
        </div>
        <!-- <div class="col-sm-6 col-md-3">
          <div class="panel panel-body panel-body-accent">
								<div class="media no-margin">
									<div class="media-body">
										<h3 class="no-margin">{{statistics.TypeCount}}</h3>
										<span class="text-uppercase text-size-mini">total types</span>
									</div>

									<div class="media-right media-middle">
										<i class="icon-bubbles4 text-success-400 icon-3x opacity-75"></i>
									</div>
								</div>
							</div>
        </div>
        
        <div class="col-sm-6 col-md-3">
          <div class="panel panel-body panel-body-accent">
            <div class="media no-margin">
              <div class="media-body">
                <h3 class="no-margin">{{statistics.OwnerFeedbackCount}}</h3>
                <span class="text-uppercase text-size-mini">total owners comments</span>
              </div>

              <div class="media-right media-middle">
                <i class="icon-bag icon-3x text-danger-400 opacity-75"></i>
              </div>
            </div>
          </div>
        </div>
        <div class="col-sm-6 col-md-3">
          <div class="panel panel-body panel-body-accent">
								<div class="media no-margin">
									<div class="media-body">
										<h3 class="no-margin">{{statistics.CustomerFeedbackCount}}</h3>
										<span class="text-uppercase text-size-mini">total customers comments</span>
									</div>

									<div class="media-right media-middle">
										<i class="icon-bubbles4 text-success-400 icon-3x opacity-75"></i>
									</div>
								</div>
							</div>
        </div>
        <div class="col-sm-6 col-md-3">
          <div class="panel panel-body panel-body-accent">
								<div class="media no-margin">
									<div class="media-body">
										<h3 class="no-margin">{{statistics.CompanyCount}}</h3>
										<span class="text-uppercase text-size-mini">total companies</span>
									</div>

									<div class="media-right media-middle">
										<i class="icon-bubbles4 icon-3x opacity-75 text-blue-400"></i>
									</div>
								</div>
							</div>
        </div>-->
      </div>
    </div>
  </div>
</template>


<script>
import * as robotService from "../../air-robot/pages/robot/api/robot-service";

export default {
  data() {
    return {
      statistics: {}
    };
  },
  async beforeMount() {
    this.statistics = (await robotService.getStatistics()).data.Data;
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