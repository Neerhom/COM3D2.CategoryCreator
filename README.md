# COM3D2.CategoryCreator
Sybaris Patcher that adds new categories to maid edit.

The patcher adds new menu categories and model slots, to extend maid customization.

List of added categories and associated models slots:

Head customization:

EYE2(folder_eye2, eye2) - none

The EYE2 (eye2, folder_eye2) is the same as normal eye categories(eye, folder_eye) and is intended for making maid with eye heterochromia. The initial release includes all eye menus from base game ported to it, and they affect right eye only. Mugen versions use EYE_R color.

Initial release includes all eye menus from base COM3D2 proted to this categoris, which affect maid right eye.

Body: 

Toenails(acctoenail) - acctoenail

Skintoon(skintoon) - none

The patcher also adds accnail models slot, for use in Nails/ネイル (accnail) category.

In addition, the patcher exposes Body category from base game, which enabled loading of bodymods from CM3D2 or created for COM3D2, with disregard of menu filename.

Initial release includes free-colored nails and toenails, and a basic toon mod.

Accessories:

Hand_L(acchandl) - acchandl

Hand_R(acchandr) - acchandr

Ears - ears

Horns - horns

Hand_L and Hand_R are categories dedicated for hand items, and initial release includes dildos from base game.

Ears and horns categories are added to make ears and horns compatible with hats, without sacrificing any other category.
The relesease includes ears and horns ported from base game.

Patcher relies on NEI file override, which is currently only enabled by [COM3D2.ModLoader](https://github.com/Neerhom/COM3D2.ModLoader)

Project's [Wiki](https://github.com/Neerhom/COM3D2.CategoryCreator/wiki) with samples of how to make mods for CategoryCreator.


Support by existing plugins: none at the time of release.


# COM3D2.Creator_SaveFix
Because of how the game writes save file data, the save files saved with COM3D2.CategoryCreator installed cannot be read by base game properly and will result in bunch of errors. The Creator_SaveFix modifies save file loading process enabling the game to handle those error, allowing for removale of COM3D2.CategoryCreator if one needs to. Do note, that removing COM3D2.CategoryCreator, would also requires removal of ALL .menu files from Mod folder that rely on it, including files shipped with it, because game won't be able to read them when loading Maid/Man edit and Photo mode and would crash instead (don't recall CM3D2 having this issue x_x).
Because of this, mod makers that make mods reliant on CategoryCreator are encouraged to distribute their mods in folders that contains Creator in the name, as to make uninstallation simpler for the user.

However, this is only true for mods made for categories created by CategoryCreators, making bodymods safe to keep if removing CategoryCreator.
