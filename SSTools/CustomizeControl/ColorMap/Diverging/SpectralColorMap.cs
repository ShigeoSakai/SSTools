﻿using System.Drawing;
namespace SSTools.ColorMap.Diverging
{
	/// <summary>
	/// Spectralカラーマップ
	/// </summary>
	public class SpectralColorMap : ColorMapClass
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public SpectralColorMap()
		{
			colorMap = spectral_map_;
		}
		/// <summary>
		/// カラーマップテーブル
		/// </summary>
		protected Color[] spectral_map_ =
		{
			Color.FromArgb(158,1,66),		// 0
			Color.FromArgb(160,3,66),		// 1
			Color.FromArgb(162,5,67),		// 2
			Color.FromArgb(164,8,67),		// 3
			Color.FromArgb(166,10,68),		// 4
			Color.FromArgb(168,12,68),		// 5
			Color.FromArgb(170,15,69),		// 6
			Color.FromArgb(173,17,69),		// 7
			Color.FromArgb(175,20,70),		// 8
			Color.FromArgb(177,22,70),		// 9
			Color.FromArgb(179,24,71),		// 10
			Color.FromArgb(181,27,71),		// 11
			Color.FromArgb(183,29,72),		// 12
			Color.FromArgb(186,32,72),		// 13
			Color.FromArgb(188,34,73),		// 14
			Color.FromArgb(190,36,73),		// 15
			Color.FromArgb(192,39,74),		// 16
			Color.FromArgb(194,41,74),		// 17
			Color.FromArgb(196,44,75),		// 18
			Color.FromArgb(198,46,75),		// 19
			Color.FromArgb(201,48,76),		// 20
			Color.FromArgb(203,51,76),		// 21
			Color.FromArgb(205,53,77),		// 22
			Color.FromArgb(207,56,77),		// 23
			Color.FromArgb(209,58,78),		// 24
			Color.FromArgb(211,60,78),		// 25
			Color.FromArgb(213,62,78),		// 26
			Color.FromArgb(214,64,78),		// 27
			Color.FromArgb(216,66,77),		// 28
			Color.FromArgb(217,68,77),		// 29
			Color.FromArgb(218,70,76),		// 30
			Color.FromArgb(219,72,76),		// 31
			Color.FromArgb(220,73,75),		// 32
			Color.FromArgb(222,75,75),		// 33
			Color.FromArgb(223,77,75),		// 34
			Color.FromArgb(224,79,74),		// 35
			Color.FromArgb(225,81,74),		// 36
			Color.FromArgb(226,83,73),		// 37
			Color.FromArgb(228,85,73),		// 38
			Color.FromArgb(229,86,72),		// 39
			Color.FromArgb(230,88,72),		// 40
			Color.FromArgb(231,90,71),		// 41
			Color.FromArgb(233,92,71),		// 42
			Color.FromArgb(234,94,70),		// 43
			Color.FromArgb(235,96,70),		// 44
			Color.FromArgb(236,97,69),		// 45
			Color.FromArgb(237,99,69),		// 46
			Color.FromArgb(239,101,68),		// 47
			Color.FromArgb(240,103,68),		// 48
			Color.FromArgb(241,105,67),		// 49
			Color.FromArgb(242,107,67),		// 50
			Color.FromArgb(244,109,67),		// 51
			Color.FromArgb(244,111,68),		// 52
			Color.FromArgb(244,114,69),		// 53
			Color.FromArgb(245,116,70),		// 54
			Color.FromArgb(245,119,71),		// 55
			Color.FromArgb(245,121,72),		// 56
			Color.FromArgb(246,124,74),		// 57
			Color.FromArgb(246,126,75),		// 58
			Color.FromArgb(246,129,76),		// 59
			Color.FromArgb(247,131,77),		// 60
			Color.FromArgb(247,134,78),		// 61
			Color.FromArgb(247,137,79),		// 62
			Color.FromArgb(248,139,81),		// 63
			Color.FromArgb(248,142,82),		// 64
			Color.FromArgb(248,144,83),		// 65
			Color.FromArgb(249,147,84),		// 66
			Color.FromArgb(249,149,85),		// 67
			Color.FromArgb(250,152,86),		// 68
			Color.FromArgb(250,154,88),		// 69
			Color.FromArgb(250,157,89),		// 70
			Color.FromArgb(251,159,90),		// 71
			Color.FromArgb(251,162,91),		// 72
			Color.FromArgb(251,165,92),		// 73
			Color.FromArgb(252,167,94),		// 74
			Color.FromArgb(252,170,95),		// 75
			Color.FromArgb(252,172,96),		// 76
			Color.FromArgb(253,174,97),		// 77
			Color.FromArgb(253,176,99),		// 78
			Color.FromArgb(253,178,101),		// 79
			Color.FromArgb(253,180,102),		// 80
			Color.FromArgb(253,182,104),		// 81
			Color.FromArgb(253,184,106),		// 82
			Color.FromArgb(253,186,107),		// 83
			Color.FromArgb(253,188,109),		// 84
			Color.FromArgb(253,190,110),		// 85
			Color.FromArgb(253,192,112),		// 86
			Color.FromArgb(253,194,114),		// 87
			Color.FromArgb(253,196,115),		// 88
			Color.FromArgb(253,198,117),		// 89
			Color.FromArgb(253,200,119),		// 90
			Color.FromArgb(253,202,120),		// 91
			Color.FromArgb(253,204,122),		// 92
			Color.FromArgb(253,206,124),		// 93
			Color.FromArgb(253,208,125),		// 94
			Color.FromArgb(253,210,127),		// 95
			Color.FromArgb(253,212,129),		// 96
			Color.FromArgb(253,214,130),		// 97
			Color.FromArgb(253,216,132),		// 98
			Color.FromArgb(253,218,134),		// 99
			Color.FromArgb(253,220,135),		// 100
			Color.FromArgb(253,222,137),		// 101
			Color.FromArgb(254,224,139),		// 102
			Color.FromArgb(254,225,141),		// 103
			Color.FromArgb(254,226,143),		// 104
			Color.FromArgb(254,227,145),		// 105
			Color.FromArgb(254,228,147),		// 106
			Color.FromArgb(254,230,149),		// 107
			Color.FromArgb(254,231,151),		// 108
			Color.FromArgb(254,232,153),		// 109
			Color.FromArgb(254,233,155),		// 110
			Color.FromArgb(254,234,157),		// 111
			Color.FromArgb(254,236,159),		// 112
			Color.FromArgb(254,237,161),		// 113
			Color.FromArgb(254,238,163),		// 114
			Color.FromArgb(254,239,165),		// 115
			Color.FromArgb(254,241,167),		// 116
			Color.FromArgb(254,242,169),		// 117
			Color.FromArgb(254,243,171),		// 118
			Color.FromArgb(254,244,173),		// 119
			Color.FromArgb(254,245,175),		// 120
			Color.FromArgb(254,247,177),		// 121
			Color.FromArgb(254,248,179),		// 122
			Color.FromArgb(254,249,181),		// 123
			Color.FromArgb(254,250,183),		// 124
			Color.FromArgb(254,251,185),		// 125
			Color.FromArgb(254,253,187),		// 126
			Color.FromArgb(254,254,189),		// 127
			Color.FromArgb(254,254,190),		// 128
			Color.FromArgb(253,254,188),		// 129
			Color.FromArgb(252,254,187),		// 130
			Color.FromArgb(251,253,185),		// 131
			Color.FromArgb(250,253,184),		// 132
			Color.FromArgb(249,252,182),		// 133
			Color.FromArgb(248,252,181),		// 134
			Color.FromArgb(247,252,179),		// 135
			Color.FromArgb(246,251,178),		// 136
			Color.FromArgb(245,251,176),		// 137
			Color.FromArgb(244,250,174),		// 138
			Color.FromArgb(243,250,173),		// 139
			Color.FromArgb(242,250,171),		// 140
			Color.FromArgb(241,249,170),		// 141
			Color.FromArgb(240,249,168),		// 142
			Color.FromArgb(239,248,167),		// 143
			Color.FromArgb(238,248,165),		// 144
			Color.FromArgb(237,248,164),		// 145
			Color.FromArgb(236,247,162),		// 146
			Color.FromArgb(235,247,161),		// 147
			Color.FromArgb(234,246,159),		// 148
			Color.FromArgb(233,246,158),		// 149
			Color.FromArgb(232,246,156),		// 150
			Color.FromArgb(231,245,155),		// 151
			Color.FromArgb(230,245,153),		// 152
			Color.FromArgb(230,245,152),		// 153
			Color.FromArgb(227,244,152),		// 154
			Color.FromArgb(225,243,152),		// 155
			Color.FromArgb(223,242,153),		// 156
			Color.FromArgb(220,241,153),		// 157
			Color.FromArgb(218,240,154),		// 158
			Color.FromArgb(216,239,154),		// 159
			Color.FromArgb(213,238,155),		// 160
			Color.FromArgb(211,237,155),		// 161
			Color.FromArgb(209,236,156),		// 162
			Color.FromArgb(206,235,156),		// 163
			Color.FromArgb(204,234,157),		// 164
			Color.FromArgb(202,233,157),		// 165
			Color.FromArgb(199,232,158),		// 166
			Color.FromArgb(197,231,158),		// 167
			Color.FromArgb(195,230,159),		// 168
			Color.FromArgb(192,229,159),		// 169
			Color.FromArgb(190,229,160),		// 170
			Color.FromArgb(188,228,160),		// 171
			Color.FromArgb(186,227,160),		// 172
			Color.FromArgb(183,226,161),		// 173
			Color.FromArgb(181,225,161),		// 174
			Color.FromArgb(179,224,162),		// 175
			Color.FromArgb(176,223,162),		// 176
			Color.FromArgb(174,222,163),		// 177
			Color.FromArgb(172,221,163),		// 178
			Color.FromArgb(169,220,164),		// 179
			Color.FromArgb(166,219,164),		// 180
			Color.FromArgb(164,218,164),		// 181
			Color.FromArgb(161,217,164),		// 182
			Color.FromArgb(158,216,164),		// 183
			Color.FromArgb(156,215,164),		// 184
			Color.FromArgb(153,214,164),		// 185
			Color.FromArgb(150,213,164),		// 186
			Color.FromArgb(148,212,164),		// 187
			Color.FromArgb(145,210,164),		// 188
			Color.FromArgb(142,209,164),		// 189
			Color.FromArgb(139,208,164),		// 190
			Color.FromArgb(137,207,164),		// 191
			Color.FromArgb(134,206,164),		// 192
			Color.FromArgb(131,205,164),		// 193
			Color.FromArgb(129,204,164),		// 194
			Color.FromArgb(126,203,164),		// 195
			Color.FromArgb(123,202,164),		// 196
			Color.FromArgb(120,201,164),		// 197
			Color.FromArgb(118,200,164),		// 198
			Color.FromArgb(115,199,164),		// 199
			Color.FromArgb(112,198,164),		// 200
			Color.FromArgb(110,197,164),		// 201
			Color.FromArgb(107,196,164),		// 202
			Color.FromArgb(104,195,164),		// 203
			Color.FromArgb(102,194,165),		// 204
			Color.FromArgb(99,191,165),		// 205
			Color.FromArgb(97,189,166),		// 206
			Color.FromArgb(95,187,167),		// 207
			Color.FromArgb(93,184,168),		// 208
			Color.FromArgb(91,182,169),		// 209
			Color.FromArgb(89,180,170),		// 210
			Color.FromArgb(87,178,171),		// 211
			Color.FromArgb(85,175,172),		// 212
			Color.FromArgb(83,173,173),		// 213
			Color.FromArgb(81,171,174),		// 214
			Color.FromArgb(79,168,175),		// 215
			Color.FromArgb(77,166,176),		// 216
			Color.FromArgb(75,164,177),		// 217
			Color.FromArgb(73,162,178),		// 218
			Color.FromArgb(71,159,179),		// 219
			Color.FromArgb(69,157,180),		// 220
			Color.FromArgb(67,155,181),		// 221
			Color.FromArgb(65,153,181),		// 222
			Color.FromArgb(63,150,182),		// 223
			Color.FromArgb(61,148,183),		// 224
			Color.FromArgb(59,146,184),		// 225
			Color.FromArgb(57,143,185),		// 226
			Color.FromArgb(55,141,186),		// 227
			Color.FromArgb(53,139,187),		// 228
			Color.FromArgb(51,137,188),		// 229
			Color.FromArgb(50,134,188),		// 230
			Color.FromArgb(52,132,187),		// 231
			Color.FromArgb(54,130,186),		// 232
			Color.FromArgb(56,128,185),		// 233
			Color.FromArgb(57,125,184),		// 234
			Color.FromArgb(59,123,183),		// 235
			Color.FromArgb(61,121,182),		// 236
			Color.FromArgb(62,119,181),		// 237
			Color.FromArgb(64,117,180),		// 238
			Color.FromArgb(66,114,178),		// 239
			Color.FromArgb(68,112,177),		// 240
			Color.FromArgb(69,110,176),		// 241
			Color.FromArgb(71,108,175),		// 242
			Color.FromArgb(73,105,174),		// 243
			Color.FromArgb(75,103,173),		// 244
			Color.FromArgb(76,101,172),		// 245
			Color.FromArgb(78,99,171),		// 246
			Color.FromArgb(80,96,170),		// 247
			Color.FromArgb(81,94,169),		// 248
			Color.FromArgb(83,92,168),		// 249
			Color.FromArgb(85,90,167),		// 250
			Color.FromArgb(87,87,166),		// 251
			Color.FromArgb(88,85,165),		// 252
			Color.FromArgb(90,83,164),		// 253
			Color.FromArgb(92,81,163),		// 254
			Color.FromArgb(94,79,162),		// 255
		};
	}
}
