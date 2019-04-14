<template>
    <div class="text-muted text-size-small">
        <span><span v-localize="{i: 'rent.from'}"></span> {{row.StartDate}} <span v-localize="{i: 'rent.to'}"></span> {{row.EndDate}} </span> <br>
        <span><span v-localize="{i: 'rent.totalCosts'}"></span> {{row.TotalPrice}}</span>
    </div>
</template>

<script>
export default {
  props: ["row", "xprops"]
};
</script>