<template>
    <div class="panel-body">
        <div class="fullcalendar-agenda fc fc-unthemed fc-ltr" style="height: 750px;"></div>
    </div>
</template>

<script>
import moment from "moment";
import fullcalendar from "fullcalendar";
import { mapGetters } from "vuex";

export default {
  data() {
    return {};
  },
  props: {
    id: {
      required: true
    }
  },
  mounted: function() {
    $(".fullcalendar-agenda")
      .fullCalendar({
        header: {
          left: "prev,next today",
          center: "title",
          right: "month"
        },
        height: "parent",
        allDaySlot: false,
        defaultView: "month",
        editable: false,
        events: {
          url: "http://localhost:53689/api/rent/getRentsForCalendarByParams",
          headers: { Authorization: `Bearer ${localStorage.token}` },
          data: {
            airTaxiId: this.id,
            isCalendarView: true
          }
        },
        timezone: "local"
      })
      .fullCalendar("render");
  }
};
</script>

<style>
.fc-right .fc-month-button {
  float: right !important;
}
</style>