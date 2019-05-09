<template>
  <div>
    <td>
      <div class="media-left media-top">
        <a class="legitRipple">
          <img v-if="row.Icon" :src="row.Icon" class="img-rounded icon-photo">
          <img
            v-else
            src="../../../../assets/limitless/images/robot.png"
            class="img-rounded icon-photo"
          >
        </a>
      </div>
      <div class="media-body">
        <h6 class="media-heading text-semibold">
          <router-link :to="'/robot-details/'+row.Id">{{row.CompanyName}} {{row.Name}}</router-link>
        </h6>
        <span class="label border-left-primary label-striped robot-lable">{{row.RobotTypeName}}</span>
        <br>
        <span class="label border-left-primary label-striped robot-lable">${{row.DailyCosts}}</span>
      </div>
    </td>
  </div>
</template>

<script>
export default {
  props: ["row", "xprops"]
};
</script>

<style>
.icon-photo {
  width: 80px !important;
  height: 75px !important;
}
.robot-lable{
    padding: 2px 6px !important;
    margin-bottom: 5px;
}
</style>
