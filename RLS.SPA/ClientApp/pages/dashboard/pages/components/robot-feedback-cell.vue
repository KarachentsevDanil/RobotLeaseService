<template>
  <div class="text-muted text-size-small">
    <strong>Last 3 Feedbacks</strong>
    <ul class="list no-padding">
      <li class="no-margin" v-for="(feedback, index) in row.Feedback" :key="index">
        <ul class="list-inline list-inline-separate text-muted no-margin">
          <li class="no-margin">{{feedback.CustomerFeedback}}</li>
          <li class="no-margin">
            <star-rating
              :inline="true"
              :star-size="12"
              :read-only="true"
              :show-rating="false"
              :rating="feedback.RobotRating"
            ></star-rating>
          </li>
        </ul>
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  props: ["row", "xprops"]
};
</script>