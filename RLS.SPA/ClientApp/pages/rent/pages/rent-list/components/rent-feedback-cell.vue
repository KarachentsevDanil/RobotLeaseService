<template>
    <div class="text-muted text-size-small">
        <span><span v-localize="{i: 'rent.ratingLabel'}"></span>: <star-rating :inline="true" :star-size="12" :read-only="true" :show-rating="false" :rating="row.RobotRating"></star-rating> </span> <br>
        <span><span v-localize="{i: 'rent.feedbackLabel'}"></span>: {{row.CustomerFeedback}}</span>
    </div>
</template>

<script>
export default {
  props: ["row", "xprops"]
};
</script>