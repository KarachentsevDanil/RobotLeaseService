<template>
  <div class="text-muted text-size-small">
    <span>
      <span v-localize="{i: 'rent.from'}"></span>
      {{row.StartDate}}
      <span v-localize="{i: 'rent.to'}"></span>
      {{row.EndDate}}
    </span>
    <br>
    <span>
      <span v-localize="{i: 'rent.totalCosts'}"></span>
      {{row.TotalPrice}}
    </span>
    <br>
    <span v-if="row.CancelReason">
      <span v-localize="{i: 'rent.cancelReason'}"></span>
      {{row.CancelReason}}
    </span>
    <br v-if="row.CancelReason">
    <span class="label" :class="getStatusClass()">{{row.Status}}</span>
  </div>
</template>

<script>
export default {
  props: ["row", "xprops"],
  methods: {
    getStatusClass() {
      switch (this.row.Status) {
        case "Created":
          return "label label-primary";

        case "Completed":
          return "label label-success";

        case "Canceled":
          return "label label-warning";

        default:
          return "";
      }
    }
  }
};
</script>