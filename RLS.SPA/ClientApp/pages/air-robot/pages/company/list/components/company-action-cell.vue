<template>
    <div class="text-center">
        <ul class="icons-list">
            <li class="dropdown dropup">
                <a href="#" class="dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                    <i class="icon-menu7"></i>
                </a>
                <ul class="dropdown-menu dropdown-menu-right">
                    <li>
                        <a><i class="icon-pencil"></i> Edit</a>
                    </li>
                    <li>
                        <a><i class="icon-bin"></i> Delete</a>
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