<template>
    <div class="text-muted text-size-small">
        <span><span>Robot Rating</span>: <star-rating :inline="true" :star-size="12" :read-only="true" :show-rating="false" :rating="row.RobotRating"></star-rating> </span> <br>
        <span><span>Customer Feedback </span>: {{row.CustomerFeedback}}</span>
        <br>
        <span><span>Customer Rating</span>: <star-rating :inline="true" :star-size="12" :read-only="true" :show-rating="false" :rating="row.CustomerRating"></star-rating> </span> <br>
        <span><span>Owner Feedback</span>: {{row.OwnerFeedback}}</span>
    </div>
</template>

<script>
export default {
  props: ["row", "xprops"]
};
</script>