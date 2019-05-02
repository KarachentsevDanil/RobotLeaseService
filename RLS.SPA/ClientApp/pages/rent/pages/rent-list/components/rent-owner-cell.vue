<template>
    <div>
        <td>
            <div class="media-left media-top">
                <span class="btn bg-pink-400 btn-rounded btn-icon btn-xs legitRipple">
                    <span class="letter-icon">{{getNameIncon()}}</span>
                </span>
            </div>
            <div class="media-body">
                <span class="display-inline-block text-semibold letter-icon-title">{{row.Owner.FirstName}} {{row.Owner.LastName}}</span>
                <div class="text-muted text-size-small">
                    <div><i class="icon-envelop2"></i> <a href="mailto:jdoe@engagesnf.com">{{row.Owner.Email}}</a></div>
                </div>
                <div class="text-muted text-size-small">
                    <div><i class="icon-mobile2"></i> <a href="#">{{row.Owner.PhoneNumber}}</a></div>
                </div>
            </div>
        </td>
    </div>
</template>

<script>
export default {
  props: ["row", "xprops"],
  methods: {
    getNameIncon() {
      return this.row.Owner.FirstName[0] + this.row.Owner.LastName[0];
    }
  }
};
</script>