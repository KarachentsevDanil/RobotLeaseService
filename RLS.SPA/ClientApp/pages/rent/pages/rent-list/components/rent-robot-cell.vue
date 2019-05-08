<template>
    <div>
        <td>
            <div class="media-left media-top">
                <a class="legitRipple">
                    <img v-if="row.Robot.Icon" :src="row.Robot.Icon" class="img-rounded icon-photo">
                    <img v-else src="../../../../../assets/limitless/images/robot.png" class="img-rounded icon-photo">
                </a>
            </div>
            <div class="media-body">
                <span class="display-inline-block text-semibold letter-icon-title">{{row.Robot.CompanyName}} {{row.Robot.ModelName}}</span>
                <div class="text-muted text-size-small">
                    <span><span v-localize="{i: 'robot.robotType'}"></span> {{row.Robot.TypeName}}</span>
                    <br>                   
                    <span><span v-localize="{i: 'robot.pricePerDay'}"></span> {{row.Robot.DailyCosts}}</span>
                </div>
            </div>
        </td>
    </div>
</template>

<script>
export default {
  props: ["row", "xprops"],
};
</script>

<style>
.icon-photo{
    width: 70px !important;
    height: 70px !important;
}
</style>
